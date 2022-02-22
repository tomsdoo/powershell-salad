function global:Get-LocalUsers([string]$computername)
#VISIBILITY:public
{
	if([string]::IsNullOrEmpty($computername))
	{
		$computername = $env:computername;
	}
	$adsic = new ADSIClient;
	$adsic = $adsic.Initialize($computername);
	$adsic.GetLocalUserNames();
}
