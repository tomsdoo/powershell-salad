function global:Enable-LocalUser([string]$computername, [parameter(mandatory=$true)][string]$user)
#VISIBILITY:public
{
	if([string]::IsNullOrEmpty($computername))
	{
		$computername = $env:computername;
	}
	$adsic = new ADSIClient;
	$adsic = $adsic.Initialize($computername);
	$adsic.EnableLocalUser($user);
}
