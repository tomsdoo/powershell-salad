function global:new([parameter(mandatory=$true)][string]$classname, [switch]$debugging)
#VISIBILITY:public
{
	if(!($CodeManager.registry | ?{$_.name -like ($classname + ".*")}))
	{
		throw ($classname + " is not defined.");
	}

	$ret = new-object psobject;
	$ret | add-member -membertype noteproperty -name ClassName -value $classname -force;

	if($debugging)
	{
		$debuglist = new ClassDebugList;
		$debuglist = $debuglist.initialize();
		$ret | add-member -membertype noteproperty -name DebugInfo -value $debuglist -force;
	}

	#Call Constructor
	$constructor = $CodeManager.registry | ?{$_.name -eq ($classname + "." + "Constructor")};
	if($constructor)
	{
		$temp = ($constructor.name -split "\.");
		$methodname = $temp[$temp.count -1];
		$impcode = $CodeManager.registry | ?{$_.name -eq $constructor.name} | %{$_.value};
		$sb = [ScriptBlock]::Create($impcode);
		$ret | add-member -membertype scriptmethod -name $methodname -value $sb -force;

		$ret = $ret.$methodname.invoke();
	}

	$inheritExecutor = new-object psobject;
	$code = '
	param($Inheritor, $InheritFrom)
	$retInheritor = $Inheritor;
	$objFrom = new $InheritFrom;
	if($objFrom.BaseClass)
	{
		foreach($bcn in $objFrom.BaseClass)
		{
			$retInheritor = $this.Inherit($retInheritor, $bcn);
		}
	}
	$methodcoderegistrylist = $CodeManager.registry | ?{$_.name -like ($InheritFrom + ".*")};
	foreach($mcr in $methodcoderegistrylist)
	{
		$temp = ($mcr.name -split "\.");
		$methodname = $temp[$temp.count -1];
		$sb = [ScriptBlock]::Create($CodeManager.GetValue($mcr.name));
		$retInheritor | add-member -membertype scriptmethod -name $methodname -value $sb -force;
	}
	$retInheritor;
	'
	$sb = [ScriptBlock]::Create($code);
	$inheritExecutor | add-member -membertype scriptmethod -name Inherit -value $sb -force;

	#Inherit
	if($ret.BaseClass)
	{
		foreach($bcn in $ret.BaseClass)
		{
			$ret = $inheritExecutor.Inherit($ret, $bcn);
		}
	}

	#Method Implementation
	$methodcoderegistrylist = $CodeManager.registry | ?{$_.name -like ($classname + ".*")};
	foreach($mcr in $methodcoderegistrylist)
	{
		$temp = ($mcr.name -split "\.");
		$methodname = $temp[$temp.count -1];

		$impcode = $CodeManager.registry | ?{$_.name -eq $mcr.name} | %{$_.value};
		if($debugging)
		{
			$splitlist = @();
			$splitlist += "\;";
			$splitlist += " ";
			$splitlist += "\.";
			$splitlist += ",";
			$splitlist += "\(";
			$splitlist += "\)";
			$splitlist += "{";
			$splitlist += "}";
			$variablenames = $impcode;
			foreach($spe in $splitlist)
			{
				$variablenames = $variablenames | %{$_ -split $spe};
			}
			$variablenames = $variablenames | ?{$_ -like "`$*"};
			$variablenames = $variablenames | ?{$_ -notlike "`$_*"};
			$variablenames = $variablenames | group | %{$_.name};

			$impcode = foreach($ie in $impcode)
			{
				$ie;
				if(($ie -notlike "param*") -and ($ie -like "*;"))
				{
					"`$containermanager = new GeneralContainerManager;";
					foreach($ve in $variablenames)
					{
						"`$container = new GeneralContainer;";
						"`$container = `$container.Initialize(`"" + ($ve -replace "\$", "") + "`", " + $ve + ");";
						"`$containermanager = `$containermanager.add(`$container);";
					}
					"`$debugele = new ClassDebugElement;";
					"`$debugele = `$debugele.initialize((`$this.debuginfo.maxseq() + 1), `$this.classname, `"" + $methodname + "`", `"" + ((($ie -replace "``", "````") -replace "`"", "```"") -replace "\$", "``$") + "`"," + "`$containermanager);";
					"`$this | add-member -membertype noteproperty -name DebugInfo -value (`$this.debuginfo.add(`$debugele)) -force;";
				}
			}
			$impcode += "`$logfnb = `$SessionManager.GetValue(`"system.session.directory.log`") + (Get-Date -format `"yyyyMMdd`") + `"_`" + `$SessionManager.GetValue(`"system.session.environment.computer`") + `"_`" + `$SessionManager.GetValue(`"system.session.environment.account`");";
			$impcode += "`$datetimestring = (get-date -format `"yyyyMMddHHmmssfffff`");";
			$impcode += "`$debuglogfilename = `$logfnb + `"_debug_`" + $datetimestring + `".xml`";";
			$impcode += "`$this.debuginfo | export-clixml `$debuglogfilename;";
			$impcode += "`$debuglist = new ClassDebugList;";
			$impcode += "`$debuglist = `$debuglist.initialize();";
			$impcode += "`$this | add-member -membertype noteproperty -name DebugInfo -value `$debuglist -force;";
		}
		$impcode = foreach($ie in $impcode)
		{
			$ie;
			if($ie -like "param*")
			{
				"try";
				"{";
			}
		}
		$impcode += "}";
		$impcode += "catch";
		$impcode += "{";
		$impcode += "`$logfnb = `$SessionManager.GetValue(`"system.session.directory.log`") + (Get-Date -format `"yyyyMMdd`") + `"_`" + `$SessionManager.GetValue(`"system.session.environment.computer`") + `"_`" + `$SessionManager.GetValue(`"system.session.environment.account`");";
		$impcode += "`$datetimestring = (get-date -format `"yyyyMMddHHmmssfffff`");";
		$impcode += "`$errorinvlogfilename = `$logfnb + `"_error_invinfo_`" + `$datetimestring + `".xml`";";
		$impcode += "`$errorreclogfilename = `$logfnb + `"_error_recinfo_`" + `$datetimestring + `".xml`";";
		$impcode += "`$myinvocation | export-clixml `$errorinvlogfilename;";
		$impcode += "`$error | export-clixml `$errorreclogfilename;";
		$impcode += "`$error.clear();";
		$impcode += "throw `"Error Occured On `" + `$this.classname + " + "`"::" + $methodname + "`";";
		$impcode += "}";
		try
		{
			$sb = [ScriptBlock]::Create($impcode);
		}
		catch
		{
return $impcode;
			throw;
		}
		$ret | add-member -membertype scriptmethod -name $methodname -value $sb -force;
	}
	$ret;
}
