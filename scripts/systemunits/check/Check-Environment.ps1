function global:Check-Environment([switch]$authorize)
#VISIBILITY:public
{
	$bootstrappathstring = "system.session.directory.bootstrap";
	$classespathstring = "system.session.directory.classes";
	$scriptspathstring = "system.session.directory.scripts";
	$definitionpathstring = "system.session.directory.definition";
	$codepathstring = "system.session.directory.definition.code";
	$contentlist = @();
	$contentlist += $bootstrappathstring;
	$contentlist += $classespathstring;
	$contentlist += $scriptspathstring;

	$list = $contentlist + $definitionpathstring;
	$contentlist += $codepathstring;

	$directoryinfofolder = ($SessionManager.GetValue("system.session.directory.definition.system") + "directoryinfo" + [System.IO.Path]::DirectorySeparatorChar.ToString());

	if($authorize)
	{
		$r = Read-Host "Authorize?(Y/N)";
		if($r.ToUpper() -eq "Y")
		{
			$fcfn = $directoryinfofolder + "filecontents.xml";
			$fcl = foreach($element in $contentlist)
			{
				$fl = get-childitem $SessionManager.GetValue($element) -recurse | ?{$_.attributes -notlike "*directory*"};
				foreach($fe in $fl)
				{
					$fo = new-object psobject;
					$fo | add-member -membertype noteproperty -name FileName -value $fe.fullname -force;
					$fc = get-content $fe.fullname;
					$fo | add-member -membertype noteproperty -name FileContent -value $fc -force;
					$fo;
				}
			}
			$fcl | export-clixml $fcfn;
			get-childitem $SessionManager.GetValue($bootstrappathstring) -recurse -force | export-clixml ($directoryinfofolder + $bootstrappathstring + ".xml");
			get-childitem $SessionManager.GetValue($classespathstring) -recurse -force | export-clixml ($directoryinfofolder + $classespathstring + ".xml");
			get-childitem $SessionManager.GetValue($scriptspathstring) -recurse -force | export-clixml ($directoryinfofolder + $scriptspathstring + ".xml");

			$defpathxml = ($directoryinfofolder + $definitionpathstring + ".xml");
			get-childitem $SessionManager.GetValue($definitionpathstring) -recurse -force | export-clixml $defpathxml;
			$tempd = import-clixml $defpathxml;
			$valid = $tempd | ?{$_.fullname -eq $defpathxml} | %{$_.lastwritetime};
			get-item $defpathxml | %{$_.lastwritetime = $valid};
		}
	}

	foreach($element in $list)
	{
		$tempfilename = [System.IO.Path]::GetTempFileName();
		get-childitem $SessionManager.GetValue($element) -recurse -force | export-clixml $tempfilename;
		$ref = import-clixml ($directoryinfofolder + $element + ".xml");
		$diff = import-clixml $tempfilename;
		remove-item $tempfilename;
		((Get-Date -Format "yyyy/MM/dd HH:mm:ss:fff") + " Now Checking Attributes About " + $element + " ...");
		Compare-Object -referenceobject $ref -differenceobject $diff;
		foreach($refe in $ref)
		{
			foreach($diffe in $diff)
			{
				if($refe.fullname -eq $diffe.fullname)
				{
					if($refe.lastwritetime -ne $diffe.lastwritetime)
					{
						($refe.fullname + " is different from the environment master.");
						("Master:" + $refe.lastwritetime.tostring());
						("Actual:" + $diffe.lastwritetime.tostring());
					}
					break;
				}
			}
		}
	}

	$fcfn = $directoryinfofolder + "filecontents.xml";
	$fcl = import-clixml $fcfn;
	foreach($element in $contentlist)
	{
		$fl = get-childitem $SessionManager.GetValue($element) -recurse -force | ?{$_.attributes -notlike "*directory*"};
		((Get-Date -Format "yyyy/MM/dd HH:mm:ss:fff") + " Now Checking Contents About " + $element + " ...");
		foreach($fe in $fl)
		{
			$ref = $fcl | ?{$_.filename -eq $fe.fullname} | %{$_.filecontent};
			$diff = get-content $fe.fullname;
			compare-object -referenceobject $ref -differenceobject $diff;
		}
	}
}
