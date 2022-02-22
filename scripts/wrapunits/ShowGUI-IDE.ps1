function global:ShowGUI-IDE()
#VISIBILITY:public
{
	$cmdstr = [string]::Empty;
	Get-Command -Type Cmdlet | %{$_.Name + "/"} | %{$cmdstr += $_};
	(New-Object SALAD.IDE.MyMDI(($SessionManager.GetValue("system.session.directory.root")), $cmdstr)).ShowDialog() | Out-Null;
}
