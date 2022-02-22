function global:Remove-LocalUserFromLocalGroup([string]$computername, [parameter(mandatory=$true)][string]$group, [parameter(mandatory=$true)][string]$user)
#VISIBILITY:public
{
	if([string]::IsNullOrEmpty($computername))
	{
		$computername = $env:computername;
	}
	$adsic = new ADSIClient;
	$adsic = $adsic.Initialize($computername);
	$adsic.RemoveLocalUserFromGroup($group, $user);
}
