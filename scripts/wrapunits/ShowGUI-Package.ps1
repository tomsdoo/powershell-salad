function global:ShowGUI-Package()
#VISIBILITY:public
{
	$e = New-Object SALAD.Package.SALADEnvClass($SessionManager.GetValue("system.session.directory.root"));
	(New-Object SALAD.Package.PackageMDIForm($e)).ShowDialog() | Out-Null;
}
