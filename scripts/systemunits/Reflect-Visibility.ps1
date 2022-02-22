function global:Reflect-Visibility()
#VISIBILITY:public
{
	$fn = $SessionManager.GetValue("system.session.directory.definition.system") + "publiccommand\public_func.txt";
	$fl = Get-ChildItem -path $SessionManager.GetValue("system.session.directory.scripts") -recurse | ?{$_.attributes -notlike "*directory*"};
	$cmlist = foreach($fe in $fl)
	{
		$fc = Get-Content $fe.fullname;
		$tof = $false;
		$fc | %{$tof = $tof -or ($_ -like "#VISIBILITY:public*")};
		if($tof)
		{
			$fe.name -replace ".ps1","";
		}
	}
	$cmlist | Set-Content $fn;
}
