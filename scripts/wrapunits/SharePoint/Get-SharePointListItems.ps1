function global:Get-SharePointListItems([parameter(mandatory=$true)][string]$SiteUrl, [parameter(mandatory=$true)][string]$ListName, [string]$ViewName, [switch]$WithAce, [switch]$WithJet, [switch]$UseDefaultCredential)
#VISIBILITY:public
{
	$engine = [string]::Empty;
	$acccount = 0;
	$methodcount = 0;
	if($WithACE -eq $true)
	{
		$engine = "ACE";
		$acccount++;
		$methodcount++;
	}
	if($WithJet -eq $true)
	{
		$engine = "JET";
		$acccount++;
		$methodcount++;
	}
	if($methodcount -ne 1)
	{
		throw "Choose one method.(WithAce/WithJet)";
	}
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
	$listguid = $listgetter.GetListGUIDFromListName($ListName);
	if([string]::IsNullOrEmpty($listguid))
	{
		throw "No such a list.";
	}
	$viewguid = [string]::Empty;
	if(!([string]::IsNullOrEmpty($ViewName)))
	{
		$viewgetter = (new SharePointGetViewCollection).Initialize($SiteUrl, $ListName);
		if($mycred -ne $null)
		{
			$viewgetter = $viewgetter.SetCredential($mycred);
		}
		$viewguid = $viewgetter.GetViewGUIDFromViewName($ViewName);
	}
	if($acccount -gt 0)
	{
		$acc = (new SharePointAccessClient).ChangeEngine($engine).SetUrl($SiteUrl).SetListGUID($listguid);
		$acc = $acc.SetViewGUID($viewguid);
		$acc.ExecuteSelectQuery("select * from list");
	}
}
