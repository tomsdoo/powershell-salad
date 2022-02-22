function global:prompt()
{
	$host.UI.RawUI.ForegroundColor = [system.consolecolor]::Red;
	$host.UI.RawUI.BackgroundColor = [system.consolecolor]::Black;

	$TranscriptFileName = ($env:currd + "log" + [system.io.path]::directoryseparatorchar.tostring() + (Get-Date -format "yyyyMMdd") + $env:computername + "_" + $env:username + "_skipper_transcript_" + (Get-Date -format "yyyyMMddHHmmssfff") + ".log");
	$dummy = Start-Transcript -path $TranscriptFileName;
	($env:username + " On " + $env:computername + " ... Environment Not Initialized ... Please Exit >");
	exit;
}
