function global:Make-Library([parameter(mandatory=$true)][string]$folder)
{
	$folderpath = $folder;
	if(([System.IO.Path]::GetFileName($folderpath)) -ne "")
	{
		$folderpath += ([System.IO.Path]::DirectorySeparatorChar.ToString());
	}
	if(!(Test-Path $folderpath))
	{
		New-Item $folderpath -Type Directory | Out-Null;
	}
	$clfoldername = "USOClasses";
	Make-ClassLibrary -folder ($folderpath + $clfoldername);
	$ffoldername = "Functions";
	Make-FunctionLibrary -folder ($folderpath + $ffoldername);
	$netfoldername = "Assembly";
	Make-NetLibrary -folder ($folderpath + $netfoldername);
	
	$indexcontent = [string]::Empty;
	$indexcontent += "<html><body>";
	$indexcontent += ("<a href=`"" + $netfoldername + "/__.htm" + "`" target=`"frcontent`">" + $netfoldername + "</a></br/>");
	$indexcontent += ("<a href=`"" + $ffoldername + "/__.htm" + "`" target=`"frcontent`">" + $ffoldername + "</a></br/>");
	$indexcontent += ("<a href=`"" + $clfoldername + "/__.htm" + "`" target=`"frcontent`">" + $clfoldername + "</a></br/>");
	$indexcontent += "</body></html>";
	$indexcontent | Set-Content ($folderpath + "index.htm");

	$x = New-Object System.Xml.XmlDocument;
	$h = $x.CreateElement("HTML");
	$x.AppendChild($h) | Out-Null;
	$f = $x.CreateElement("frameset");
	$h.AppendChild($f) | Out-Null;
	$f.SetAttribute("rows", "20%,*");
	$fr = $x.CreateElement("frame");
	$fr.SetAttribute("src", "index.htm");
	$fr.SetAttribute("name", "frindex");
	$f.AppendChild($fr) | Out-Null;
	$fr = $x.CreateElement("frame");
	$fr.SetAttribute("src", "index.htm");
	$fr.SetAttribute("name", "frcontent");
	$f.AppendChild($fr) | Out-Null;
	$myfn = $folderpath + "__.htm";
	$x.OuterXml | Set-Content $myfn;
	Invoke-Item $myfn;
}
