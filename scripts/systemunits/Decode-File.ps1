function global:Decode-File([parameter(mandatory=$true)][string]$inputfilename, [parameter(mandatory=$true)][string]$outputfilename, [parameter(mandatory=$true)][string]$keystring)
#VISIBILITY:public
{
	$decoder = new EnDecode;
	$decoder = $decoder.Initialize($keystring);
	$decoder.FileDecode($inputfilename, $outputfilename);
}
