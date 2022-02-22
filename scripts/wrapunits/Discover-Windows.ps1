function global:Discover-Windows($name)
#VISIBILITY:public
{
	$name = @($input);
	$l = $name | %{new-object salad.discovery.windowscomputerclass($_);}
	$l | %{$_.initialize();}
	while($true)
	{
		[System.Threading.Thread]::Sleep(10);
		$notyet = $l | ?{!$_.discovered}
		if($notyet -eq $null)
		{
			break;
		}
	}
	$l;
}
