function global:splashcore($logo, $nickname)
{
	$outlogo = @();
	for($i = 0; $i -lt $logo.count; $i++)
	{
		$outline = "";
		for($j = 0; $j -lt $logo[$i].length; $j++)
		{
			Clear-Host;
			$outlogo;
			$outline += $logo[$i][$j].ToString();
			$outline;
			[System.Threading.Thread]::Sleep(200);
		}
		$outlogo += $logo[$i];
	}

	Clear-Host;
	$outlogo;

	$characters = for($i = 0; $i -lt $outlogo.count; $i++)
	{
		for($j = 0; $j -lt $outlogo[$i].length; $j++)
		{
			$outlogo[$i][$j];
		}
	}
	$repcand = $characters | group | %{$_.name} | sort;

	foreach($repc in $repcand)
	{
		Clear-Host;
		$outlogo | %{$_ -replace $repc, " "};
		[System.Threading.Thread]::Sleep(200);
	}

	$replogo = $outlogo;
	foreach($repc in $repcand)
	{
		$bnotproc = $false;
		for($i = 0; $i -lt $nickname.length; $i++)
		{
			$bnotproc = $bnotproc -or ($nickname[$i].ToString().ToUpper() -eq $repc.ToString().ToUpper());
		}
		if(!$bnotproc)
		{
			Clear-Host;
			$replogo = $replogo | %{$_ -replace $repc, " "};
			$replogo;
			[System.Threading.Thread]::Sleep(300);
		}
	}

	$outnickname = "";
	for($i = 0; $i -lt $nickname.length; $i++)
	{
		Clear-Host;
		$outnickname += $nickname[$i].ToString();
		$outnickname;
		[System.Threading.Thread]::Sleep(300);
	}
	$nowaforecolor = $host.UI.RawUI.ForegroundColor;
	$colorlist = [System.ConsoleColor]::GetValues([System.ConsoleColor]);
	foreach($cole in $colorlist)
	{
		$host.UI.RawUI.ForegroundColor = $cole;
		Clear-Host;
		$outnickname;
		[System.Threading.Thread]::Sleep(300);
	}
	$host.UI.RawUI.ForegroundColor = $nowaforecolor;

	Clear-Host;
}
