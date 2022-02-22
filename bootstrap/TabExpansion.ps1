function global:TabExpansion($line , $lastWord)
{
                function Write-Members ($sep='.')
                {
                    Invoke-Expression ('$_val=' + $_expression)

                    $_method = [Management.Automation.PSMemberTypes] `
                        'Method,CodeMethod,ScriptMethod,ParameterizedProperty'
                    if ($sep -eq '.')
                    {
                        $params = @{view = 'extended','adapted','base'}
                    }
                    else
                    {
                        $params = @{static=$true}
                    }
        
                    foreach ($_m in ,$_val | Get-Member @params $_pat |
                        Sort-Object membertype,name)
                    {
                        if ($_m.MemberType -band $_method)
                        {
                            # Return a method...
                            $_base + $_expression + $sep + $_m.name + '('
                        }
                        else {
                            # Return a property...
                            $_base + $_expression + $sep + $_m.name
                        }
                    }
                }

                # If a command name contains any of these chars, it needs to be quoted
                $_charsRequiringQuotes = ('`&@''#{}()$,;|<> ' + "`t").ToCharArray()

                # If a variable name contains any of these characters it needs to be in braces
                $_varsRequiringQuotes = ('-`&@''#{}()$,;|<> .\/' + "`t").ToCharArray()

                switch -regex ($lastWord)
                {
                    # Handle property and method expansion rooted at variables...
                    # e.g. $a.b.<tab>
                    '(^.*)(\$(\w|:|\.)+)\.([*\w]*)$' {
                        $_base = $matches[1]
                        $_expression = $matches[2]
                        $_pat = $matches[4] + '*'
                        Write-Members
                        break;
                    }

                    # Handle simple property and method expansion on static members...
                    # e.g. [datetime]::n<tab>
                    '(^.*)(\[(\w|\.|\+)+\])(\:\:|\.){0,1}([*\w]*)$' {
                        $_base = $matches[1]
                        $_expression = $matches[2]
                        $_pat = $matches[5] + '*'
                        Write-Members $(if (! $matches[4]) {'::'} else {$matches[4]})
                        break;
                    }

                    # Handle complex property and method expansion on static members
                    # where there are intermediate properties...
                    # e.g. [datetime]::now.d<tab>
                    '(^.*)(\[(\w|\.|\+)+\](\:\:|\.)(\w+\.)+)([*\w]*)$' {
                        $_base = $matches[1]  # everything before the expression
                        $_expression = $matches[2].TrimEnd('.') # expression less trailing '.'
                        $_pat = $matches[6] + '*'  # the member to look for...
                        Write-Members
                        break;
                    }

                    # Handle variable name expansion...
                    '(^.*\$)([*\w:]+)$' {
                        $_prefix = $matches[1]
                        $_varName = $matches[2]
                        $_colonPos = $_varname.IndexOf(':')
                        if ($_colonPos -eq -1)
                        {
                            $_varName = 'variable:' + $_varName
                            $_provider = ''
                        }
                        else
                        {
                            $_provider = $_varname.Substring(0, $_colonPos+1)
                        }

                        foreach ($_v in Get-ChildItem ($_varName + '*') | sort Name)
                        { 
                            $_nameFound = $_v.name
                            $(if ($_nameFound.IndexOfAny($_varsRequiringQuotes) -eq -1) {'{0}{1}{2}'}
                            else {'{0}{{{1}{2}}}'}) -f $_prefix, $_provider, $_nameFound
                        }
                        break;
                    }

                    # Do completion on parameters...
                    '^-([*\w0-9]*)' {
                        $_pat = $matches[1] + '*'

                        # extract the command name from the string
                        # first split the string into statements and pipeline elements
                        # This doesn't handle strings however.
                        $_command = [regex]::Split($line, '[|;=]')[-1]

                        #  Extract the trailing unclosed block e.g. ls | foreach { cp
                        if ($_command -match '\{([^\{\}]*)$')
                        {
                            $_command = $matches[1]
                        }

                        # Extract the longest unclosed parenthetical expression...
                        if ($_command -match '\(([^()]*)$')
                        {
                            $_command = $matches[1]
                        }

                        # take the first space separated token of the remaining string
                        # as the command to look up. Trim any leading or trailing spaces
                        # so you don't get leading empty elements.
                        $_command = $_command.TrimEnd('-')
                        $_command,$_arguments = $_command.Trim().Split()

                        # now get the info object for it, -ArgumentList will force aliases to be resolved
                        # it also retrieves dynamic parameters
                        try
                        {
                            $_command = @(Get-Command -type 'Alias,Cmdlet,Function,Filter,ExternalScript' `
                                -Name $_command -ArgumentList $_arguments)[0]
                        }
                        catch
                        {
                            # see if the command is an alias. If so, resolve it to the real command
                            if(Test-Path alias:\$_command)
                            {
                                $_command = @(Get-Command -Type Alias $_command)[0].Definition
                            }

                            # If we were unsuccessful retrieving the command, try again without the parameters
                            $_command = @(Get-Command -type 'Cmdlet,Function,Filter,ExternalScript' `
                                -Name $_command)[0]
                        }

                        # remove errors generated by the command not being found, and break
                        if(-not $_command) { $error.RemoveAt(0); break; }

                        # expand the parameter sets and emit the matching elements
                        # need to use psbase.Keys in case 'keys' is one of the parameters
                        # to the cmdlet
                        foreach ($_n in $_command.Parameters.psbase.Keys)
                        {
                            if ($_n -like $_pat) { '-' + $_n }
                        }
                        break;
                    }

                    # Tab complete against history either #<pattern> or #<id>
                    '^#(\w*)' {
                        $_pattern = $matches[1]
                        if ($_pattern -match '^[0-9]+$')
                        {
                            Get-History -ea SilentlyContinue -Id $_pattern | Foreach { $_.CommandLine } 
                        }
                        else
                        {
                            $_pattern = '*' + $_pattern + '*'
                            Get-History -Count 32767 | Sort-Object -Descending Id| Foreach { $_.CommandLine } | where { $_ -like $_pattern }
                        }
                        break;
                    }

                    # try to find a matching command...
                    default {
                        # parse the script...
                        $_tokens = [System.Management.Automation.PSParser]::Tokenize($line,
                            [ref] $null)

                        if ($_tokens)
                        {
                            $_lastToken = $_tokens[$_tokens.count - 1]
                            if ($_lastToken.Type -eq 'Command')
                            {
                                $_cmd = $_lastToken.Content

                                # don't look for paths...
                                if ($_cmd.IndexOfAny('/\:') -eq -1)
                                {
                                    # handle parsing errors - the last token string should be the last
                                    # string in the line...
                                    if ($lastword.Length -ge $_cmd.Length -and 
                                        $lastword.substring($lastword.length-$_cmd.length) -eq $_cmd)
                                    {
                                        $_pat = $_cmd + '*'
                                        $_base = $lastword.substring(0, $lastword.length-$_cmd.length)

                                        # get files in current directory first, then look for commands...
                                        $( try {Resolve-Path -ea SilentlyContinue -Relative $_pat } catch {} ;
                                           try { $ExecutionContext.InvokeCommand.GetCommandName($_pat, $true, $false) |
                                               Sort-Object -Unique } catch {} ) |
                                                   # If the command contains non-word characters (space, ) ] ; ) etc.)
                                                   # then it needs to be quoted and prefixed with &
                                                   ForEach-Object {
                                                        if ($_.IndexOfAny($_charsRequiringQuotes) -eq -1) { $_ }
                                                        elseif ($_.IndexOf('''') -ge 0) {'& ''{0}''' -f $_.Replace('''','''''') }
                                                        else { '& ''{0}''' -f $_ }} |
                                                   ForEach-Object {'{0}{1}' -f $_base,$_ }
                                    }
                                }
                            }
                            if($GLib -ne $null)
                            {
				$GLib.GetAvailableClassNames() | ?{$_ -like ($lastWord + "*")};
                            }
                            $SessionManager.registry | %{$_.name} | ?{$_ -like ($lastWord + "*")};
                        }
                        else
                        {
				$mylastword = $lastWord;
				$mylastword = $mylastword -replace "\[", "``[";
				$mylastword = $mylastword -replace "\]", "``]";
				$SessionManager.registry | %{$_.name} | ?{$_ -like ($mylastword + "*")};
				$SessionManager.registry | ?{$_.name -like ($mylastword + "*")} | %{$_.value};
                        }
                    }
                }
            }
        
