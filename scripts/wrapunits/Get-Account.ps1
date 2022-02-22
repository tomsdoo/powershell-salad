function global:Get-Account([string]$sid, [string]$domain, [string]$username)
#VISIBILITY:public
{
	$paramcount = 0;
	$paramcount += [convert]::ToInt32(!([string]::IsNullOrEmpty($sid))) * 2;
	$paramcount += [convert]::ToInt32(!([string]::IsNullOrEmpty($domain)));
	$paramcount += [convert]::ToInt32(!([string]::IsNullOrEmpty($username)));
	if($paramcount -ne 2)
	{
		throw "It's necessary Domain and UserName, or only SID inputted.";
	}
	if(![string]::IsNullOrEmpty($sid))
	{
		(new SID).Initialize($sid);
	}
	else
	{
		(new NTAccount).Initialize($domain, $username);
	}
}
