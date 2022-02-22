function global:End-Macro()
#VISIBILITY:public
{
	$pr = "Record now?(Y/N)";
	$dlgr = Read-Host $pr;
	if(($dlgr.ToUpper()) -eq "Y")
	{
		$cont = Get-History | %{$_.commandline};
		$cont = $cont -replace "Start-Macro","";
		$datetimestring = (Get-Date -format "yyyyMMddHHmmss");
		$fn = $SessionManager.GetValue("system.session.directory.macrounits") + "Macro-" + $SessionManager.GetValue("system.session.environment.account") + $datetimestring + ".ps1";
		"function global:Macro-" + $SessionManager.GetValue("system.session.environment.account") + $datetimestring + "()" | Set-Content $fn;
		"{" | Add-Content $fn;
		$cont | Add-Content $fn;
		"}" | Add-Content $fn;
		Import-Module $fn;
	}
}
