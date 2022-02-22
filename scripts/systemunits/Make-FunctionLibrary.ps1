function global:Make-FunctionLibrary([parameter(mandatory=$true)][string]$folder)
#VISIBILITY:public
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

	$fi = new FunctionInterpreter;
	$fl = $fi.analyze();

	foreach($fe in $fl.List)
	{
		$tempproplist = "Parameter","calls","News";
		$tablehtml = @();
		foreach($pe in $tempproplist)
		{
			$templist = for($i = 0; $i -lt ($fe.$pe).count; $i++)
			{
				$tempparam = new-object psobject;
				$tempparam | add-member -membertype noteproperty -name Seq -value $i -force;
				$tempparam | add-member -membertype noteproperty -name $pe -value ($fe.$pe)[$i] -force;
				$tempparam;
			}
			$html = $templist | convertto-html;

			$bcontain = $false;
			$binsertcap = $false;
			foreach($htmle in $html)
			{
				switch($htmle.ToUpper())
				{
					"<TABLE>"
					{
						$bcontain = $true;
						$binsertcap = $true;
					}
					default
					{
						#nop
					}
				}
				if($bcontain)
				{
					$tablehtml += $htmle;
					if($binsertcap)
					{
						$cap = "<caption>" + $pe + "</caption>";
						$tablehtml += $cap;
						$binsertcap = $false;
					}
				}
				switch($htmle.ToUpper())
				{
					"</TABLE>"
					{
						$bcontain = $false;
					}
					default
					{
						#nop
					}
				}
			}
			$tablehtml += "<BR>";
		}

		$nullhtml = $null | convertto-html -title $fe.name -body ($fe.name) -head ($SessionManager.GetValue("system.session.definition.html.style"));
		$bcontain = $true;
		$funchtml = @();
		foreach($nullhtmle in $nullhtml)
		{
			switch($nullhtmle.ToUpper())
			{
				"<TABLE>"
				{
					$bcontain = $false;
					$funchtml += $tablehtml;
				}
				default
				{
					#nop
				}
			}
			if($bcontain)
			{
				$funchtml += $nullhtmle;
			}
			switch($nullhtmle.ToUpper())
			{
				"</TABLE>"
				{
					$bcontain = $true;
				}
				default
				{
					#nop
				}
			}
		}
		$funchtml | set-content ($folderpath + $fe.name + ".htm");
	}

	$nh = $null | convertto-html -title "LibraryIndex" -body "LibraryIndex" -head ($SessionManager.GetValue("system.session.definition.html.style"));
	$fl = $fl.List | Sort-Object Name | %{"<a href=`"" + $_.name + ".htm`" target=`"frc`">" + $_.name + "</a><br>"};
	$fl = "<br>" + $fl;
	$indexhtml = @();
	$bcontain = $true;
	foreach($nhe in $nh)
	{
		switch($nhe.ToUpper())
		{
			"<TABLE>"
			{
				$bcontain = $false;
				$indexhtml += $fl;
			}
			default
			{
				#nop
			}
		}
		if($bcontain)
		{
			$indexhtml += $nhe;
		}
		switch($nhe.ToUpper())
		{
			"</TABLE>"
			{
				$bcontain = $true;
			}
			default
			{
				#nop
			}
		}
	}
	$indexhtml | set-content ($folderpath + "index.htm");
	$x = New-Object System.Xml.XmlDocument;
	$h = $x.CreateElement("HTML");
	$x.AppendChild($h) | Out-Null;
	$f = $x.CreateElement("frameset");
	$h.AppendChild($f) | Out-Null;
	$f.SetAttribute("cols", "30%,*");
	$fr = $x.CreateElement("frame");
	$fr.SetAttribute("src", "index.htm");
	$fr.SetAttribute("name", "fri");
	$f.AppendChild($fr) | Out-Null;
	$fr = $x.CreateElement("frame");
	$fr.SetAttribute("src", "index.htm");
	$fr.SetAttribute("name", "frc");
	$f.AppendChild($fr) | Out-Null;
	$x.OuterXml | Set-Content ($folderpath + "__.htm");
}
