function global:Get-ErrorLog([string]$user, [string]$datetimestr)
#VISIBILITY:public
{
	$ec = $null;
	if(([string]::IsNullOrEmpty($user)))
	{
		$ec = (new ErrorLogCollector).Initialize();
	}
	else
	{
		$ec = (new ErrorLogCollector).Initialize($user);
	}

	if([string]::IsNullOrEmpty($datetimestr))
	{
		$ec.GetLog();
	}
	else
	{
		$ec.GetLog($datetimestr);
	}
}
