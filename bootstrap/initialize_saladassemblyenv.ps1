param()
$e = New-Object SALAD.Package.SALADEnvClass($SessionManager.GetValue("system.session.directory.root"));
foreach($myc in $e.netclassl.e)
{
	$global:SessionManager = $SessionManager.Add($myc.fullname, $myc.fullname);
}
