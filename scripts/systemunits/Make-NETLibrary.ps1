function global:Make-NETLibrary([parameter(mandatory=$true)][string]$folder)
#VISIBILITY:public
{
	$folderpath = $folder;
	if(([System.IO.Path]::GetFileName($folderpath)) -ne "")
	{
		$folderpath += ([System.IO.Path]::DirectorySeparatorChar.ToString());
	}
	if(!(Test-Path $folderpath))
	{
		New-Item $folderpath -Type Directory | Out-Null;
	}
	$asm = [System.Reflection.Assembly]::GetAssembly("SALAD.Assembly.AssemblyEClass");
	$ae = New-Object SALAD.Assembly.AssemblyEClass([int]::MinValue, $asm);
	$ae.OutHTML($folderpath);
}
