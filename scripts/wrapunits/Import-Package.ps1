function global:Import-Package([parameter(mandatory=$true)][string]$filename)
#VISIBILITY:public
{
	$x = New-Object System.Xml.XmlDocument;
	$x.Load($filename);
	$pack = New-Object SALAD.Package.PackClass($x.DocumentElement);
	$pack.Import($SessionManager.GetValue("system.session.directory.root"));
}
