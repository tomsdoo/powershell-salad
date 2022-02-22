function global:Define-Colors()
#VISIBILITY:public
{
	$templ = @();
	$colors = [system.consolecolor]::getvalues([system.consolecolor]);
	foreach($colore in $colors)
	{
		foreach($colorf in $colors)
		{
			if($colore -ne $colorf)
			{
				$host.ui.rawui.backgroundcolor = $colore;
				$host.ui.rawui.foregroundcolor = $colorf;
				clear-host;
				Get-Command Get* | %{write-host $_.definition};
				$r = read-host "If you like this, name it.(and if don't like this, just push enter)";
				if($r -ne [string]::empty)
				{
					$tempo = new-object psobject;
					$tempo | add-member -membertype noteproperty -name Name -value $r -force;
					$tempo | add-member -membertype noteproperty -name Back -value $colore -force;
					$tempo | add-member -membertype noteproperty -name Fore -value $colorf -force;
					$templ += $tempo;
				}
			}
		}
	}

	$functionsb = New-Object System.Text.StringBuilder;
	$functionsb.Append("function global:Change-Colors(") | Out-Null;
	$bfirst = $true;
	foreach($tempe in $templ)
	{
		if($bfirst -eq $true)
		{
			$bfirst = $false;
		}
		else
		{
			$functionsb.Append(",") | Out-Null;
		}
		$functionsb.Append("[switch]`$" + $tempe.name) | Out-Null;
	}
	$functionsb.Append(")") | Out-Null;
	$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	$functionsb.Append("#VISIBILITY:public") | Out-Null;
	$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	$functionsb.Append("{") | Out-Null;
	$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	foreach($tempe in $templ)
	{
		$functionsb.Append("`t") | Out-Null;
		$functionsb.Append("if(`$" + $tempe.name + " -eq `$true)") | Out-Null;
		$functionsb.Append([System.Environment]::NewLine) | Out-Null;
		$functionsb.Append("`t") | Out-Null;
		$functionsb.Append("{") | Out-Null;
		$functionsb.Append([System.Environment]::NewLine) | Out-Null;
		$functionsb.Append("`t`t") | Out-Null;
		$functionsb.Append("`$host.ui.rawui.BackgroundColor = [System.ConsoleColor]::" + $tempe.back.ToString() + ";") | Out-Null;
		$functionsb.Append([System.Environment]::NewLine) | Out-Null;
		$functionsb.Append("`t`t") | Out-Null;
		$functionsb.Append("`$host.ui.rawui.ForegroundColor = [System.ConsoleColor]::" + $tempe.fore.ToString() + ";") | Out-Null;
		$functionsb.Append([System.Environment]::NewLine) | Out-Null;
		$functionsb.Append("`t") | Out-Null;
		$functionsb.Append("}") | Out-Null;
		$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	}
	$functionsb.Append("`t") | Out-Null;
	$functionsb.Append("Clear-Host;") | Out-Null;
	$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	$functionsb.Append("}") | Out-Null;
	$functionsb.Append([System.Environment]::NewLine) | Out-Null;
	$functionsb.tostring() | Set-Content ($SessionManager.GetValue("system.session.directory.scripts") + "Change-Colors.ps1");
}
