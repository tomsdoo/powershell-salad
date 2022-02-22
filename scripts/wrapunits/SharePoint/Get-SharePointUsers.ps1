function global:Get-SharePointUsers([parameter(mandatory=$true)][string]$SiteUrl, [switch]$UseDefaultCredential, [switch]$AllProperties)
#VISIBILITY:public
{
	$mycred = $null;
	if(!($UseDefaultCredential -eq $true))
	{
		$mycred = Get-Credential $env:username;
	}
	$getter = (new SharePointGetUserCollectionFromSite).Initialize($SiteUrl);
	if($mycred -ne $null)
	{
		$getter = $getter.SetCredential($mycred);
	}
	$rl = $getter.Get();
	if($AllProperties -eq $true)
	{
		$rl;
	}
	else
	{
		$rl | Select-Object ID,Sid,Name,LoginName,Email;
	}
}
