function global:Encode-String([parameter(mandatory=$true)][string]$string, [parameter(mandatory=$true)][string]$keystring)
#VISIBILITY:public
{
	$encoder = new EnDecode;
	$encoder = $encoder.Initialize($keystring);
	$encoder.Encode($string);
}
