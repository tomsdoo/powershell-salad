function global:Get-SharePointListViews([parameter(mandatory=$true)][string]$SiteUrl, [parameter(mandatory=$true)][string]$ListName, [switch]$UseDefaultCredential, [switch]$AllProperties)
#VISIBILITY:public
{
	$mycred = $null;
	if(!($UseDefaultCredential -eq $true))
	{
		$mycred = Get-Credential $env:username;
	}

	$viewgetter = (new SharePointGetViewCollection).Initialize($SiteUrl, $ListName);
	if($mycred -ne $null)
	{
		$viewgetter = $viewgetter.SetCredential($mycred);
	}
	$rl = $viewgetter.Get();
	if($AllProperties -eq $true)
	{
		$rl;
	}
	else
	{
		$rl | Select-Object DisplayName,Name,Type,Url;
	}
}
