function global:Set-LocalUserPassword([string]$computername, [parameter(mandatory=$true)][string]$user, [parameter(mandatory=$true)][string]$password)
#VISIBILITY:public
{
	if([string]::IsNullOrEmpty($computername))
	{
		$computername = $env:computername;
	}
	$adsic = new ADSIClient;
	$adsic = $adsic.Initialize($computername);
	$adsic.SetLocalUserPassword($user, $password);
}
