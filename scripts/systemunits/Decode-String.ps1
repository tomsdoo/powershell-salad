function global:Decode-String([parameter(mandatory=$true)][string]$string, [parameter(mandatory=$true)][string]$keystring)
#VISIBILITY:public
{
	$decoder = new EnDecode;
	$decoder = $decoder.Initialize($keystring);
	$decoder.Decode($string);
}
