function global:Change-Colors([switch]$greenonblack,[switch]$yellowonblack,[switch]$whiteonblue)
#VISIBILITY:public
{
	if($greenonblack -eq $true)
	{
		$host.ui.rawui.BackgroundColor = [System.ConsoleColor]::Black;
		$host.ui.rawui.ForegroundColor = [System.ConsoleColor]::Green;
	}
	if($yellowonblack -eq $true)
	{
		$host.ui.rawui.BackgroundColor = [System.ConsoleColor]::Black;
		$host.ui.rawui.ForegroundColor = [System.ConsoleColor]::Yellow;
	}
	if($whiteonblue -eq $true)
	{
		$host.ui.rawui.BackgroundColor = [System.ConsoleColor]::DarkBlue;
		$host.ui.rawui.ForegroundColor = [System.ConsoleColor]::White;
	}
	Clear-Host;
}

