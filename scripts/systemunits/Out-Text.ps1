function global:Out-Text($any)
#VISIBILITY:public
{
	$any = @($input);
	$fn = TempFile txt;
	$any | Out-File $fn;
	Invoke-Item $fn;
}
