function global:TempFile($extension)
#VISIBILITY:public
{
	$ret = [System.IO.Path]::GetTempFileName();
	if($extension -ne $null)
	{
		$ret += "." + $extension;
	}
	$ret;
}
