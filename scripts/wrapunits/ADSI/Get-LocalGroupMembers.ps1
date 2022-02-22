function global:Get-LocalGroupMembers([string]$computername, [parameter(mandatory=$true)][string]$group)
#VISIBILITY:public
{
	if([string]::IsNullOrEmpty($computername))
	{
		$computername = $env:computername;
	}
	$adsic = new ADSIClient;
	$adsic = $adsic.Initialize($computername);
	$adsic.GetMemberOf($group);
}
