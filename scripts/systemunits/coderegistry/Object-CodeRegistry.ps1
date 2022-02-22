function global:Object-CodeRegistry([parameter(mandatory=$true)][string]$name, $value)
#VISIBILITY:public
{
	$ret = new-object psobject;
	$ret | add-member -membertype noteproperty -name Name -value $name;
	$ret | add-member -membertype noteproperty -name Value -value $value;
	$ret;
}
