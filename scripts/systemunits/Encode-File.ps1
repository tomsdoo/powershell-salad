function global:Encode-File([parameter(mandatory=$true)][string]$inputfilename, [parameter(mandatory=$true)][string]$outputfilename, [parameter(mandatory=$true)][string]$keystring)
#VISIBILITY:public
{
	$encoder = new EnDecode;
	$encoder = $encoder.Initialize($keystring);
	$encoder.FileEncode($inputfilename, $outputfilename);
}
