function global:Decode-SecureString([parameter(mandatory=$true)][System.Security.SecureString]$secstring)
#VISIBILITY:public
{
	[SALAD.BL.Util]::ConvertSecureToPlain($secstring);
}
