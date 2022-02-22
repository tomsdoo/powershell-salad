function global:Get-SharePointLists([parameter(mandatory=$true)][string]$SiteUrl, [switch]$UseDefaultCredential, [switch]$AllProperties)
#VISIBILITY:public
{
	$mycred = $null;
	if(!($UseDefaultCredential -eq $true))
	{
		$mycred = Get-Credential $env:username;
	}

	$listgetter = (new SharePointGetListCollection).Initialize($SiteUrl);
	if($mycred -ne $null)
	{
		$listgetter = $listgetter.SetCredential($mycred);
	}
	$rl = $listgetter.Get();
	if($AllProperties -eq $true)
	{
		$rl;
	}
	else
	{
		$rl | Select-Object id,title,defaultviewurl,description,created,modified,lastdeleted;
	}
}
