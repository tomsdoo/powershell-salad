function global:Get-TraceLog([string]$user, [string]$datestr)
#VISIBILITY:public
{
	$tc = $null;
	if(([string]::IsNullOrEmpty($user)))
	{
		$tc = (new TraceLogCollector).Initialize();
	}
	else
	{
		$tc = (new TraceLogCollector).Initialize($user);
	}

	if([string]::IsNullOrEmpty($datestr))
	{
		$tc.GetLog();
	}
	else
	{
		$tc.GetLog($datestr);
	}
}
