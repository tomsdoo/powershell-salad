function global:Object-SessionRegistry([parameter(mandatory=$true)][string]$name, [string]$value)
#VISIBILITY:public
{
	$ret = new-object psobject;
	$ret | add-member -membertype noteproperty -name Name -value $name -force;
	$ret | add-member -membertype noteproperty -name Value -value $value -force;

	$code = '
	param()
	"@_" + $this.name + "_@";
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name ReplaceCandidate -value $sb -force;

	$ret;
}
