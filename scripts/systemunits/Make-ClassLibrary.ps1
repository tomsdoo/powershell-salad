function global:Make-ClassLibrary([parameter(mandatory=$true)][string]$folder)
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

	$l = new Librarian;
	$cl = $l.GetAvailableClassNames();
	foreach($ce in $cl)
	{
		$i = new ClassInterpreter;
		$i = $i.Initialize($ce);
		$ml = $i.GetMethodNames();
		$tabhtml = foreach($mele in $ml)
		{
			$pl = $i.GetParameters($i.GetMethodCode($mele));
			$html = $pl.List | select Seq, Name | ConvertTo-Html;
			$tablehtml = @();
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
						$cap = "<caption>" + $mele + "</caption>";
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
			$tablehtml;
		}

		$basedon = " BasedOn ";
		$i.GetBaseClassNames() | %{$basedon = $basedon + ":" + $_};
		$classinfo = $ce + $basedon;

		$nullhtml = $null | convertto-html -title $classinfo -body ($classinfo) -head ($SessionManager.GetValue("system.session.definition.html.style"));
		$bcontain = $true;
		$classhtml = @();
		foreach($nullhtmle in $nullhtml)
		{
			switch($nullhtmle.ToUpper())
			{
				"<TABLE>"
				{
					$bcontain = $false;
					$classhtml += $tabhtml;
				}
				default
				{
					#nop
				}
			}
			if($bcontain)
			{
				$classhtml += $nullhtmle;
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
		$classhtml | set-content ($folderpath + $ce + ".htm");
	}
	$nh = $null | convertto-html -title "LibraryIndex" -body "LibraryIndex" -head ($SessionManager.GetValue("system.session.definition.html.style"));
	$cl = $cl | %{"<a href=`"" + $_ + ".htm`" target=`"frc`">" + $_ + "</a><br>"};
	$cl = "<br>" + $cl;
	$indexhtml = @();
	$bcontain = $true;
	foreach($nhe in $nh)
	{
		switch($nhe.ToUpper())
		{
			"<TABLE>"
			{
				$bcontain = $false;
				$indexhtml += $cl;
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
