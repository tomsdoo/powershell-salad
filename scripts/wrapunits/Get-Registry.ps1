function global:Get-Registry([string]$computername, [switch]$HKEY_CLASSES_ROOT, [switch]$HKEY_LOCAL_MACHINE, [switch]$HKEY_USERS, [parameter(mandatory=$true)][string]$path, [string]$name, [switch]$tree)
#VISIBILITY:public
{
	$hive = [string]::Empty;
	$count = 0;
	if($HKEY_CLASSES_ROOT -eq $true)
	{
		$hive = "HKEY_CLASSES_ROOT";
		$count++;
	}
	if($HKEY_LOCAL_MACHINE -eq $true)
	{
		$hive = "HKEY_LOCAL_MACHINE";
		$count++;
	}
	if($HKEY_USERS -eq $true)
	{
		$hive = "HKEY_USERS";
		$count++;
	}
	if($count -ne 1)
	{
		throw "Choose one hive.";
	}
	if($computername -ne $null)
	{
		$computername = $env:computername;
	}
	$rh = new RegistryHandler;
	$rh = $rh.Initialize($computername);
	if($tree -eq $true)
	{
		$rh.GetTree($hive, $path);
	}
	else
	{
		if([string]::IsNullOrEmpty($name))
		{
			throw "Input name.";
		}
		else
		{
			$rh.GetValue($hive, $path, $name);
		}
	}
}
