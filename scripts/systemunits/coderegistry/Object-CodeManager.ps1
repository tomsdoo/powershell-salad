function global:Object-CodeManager()
#VISIBILITY:public
{
	$temp = @();
	$ret = New-Object psobject;
	$ret | Add-Member -membertype noteproperty -name registry -value $temp -force;

	$code = '
	param($name)
	$this.registry | ?{$_.name -eq $name};
	'
	$sb = [scriptblock]::create($code);
	$ret | Add-Member -membertype scriptmethod -name Find -value $sb -force;

	$code = '
	param($name, $value)
	$r = object-coderegistry -name $name -value $value;
	if($this.find($name) -ne $null)
	{
		$this = $this.remove($name);
	}
	$temp = @();
	$temp += $this.registry;
	$temp += $r;
	$this.registry = $temp | sort name;
	$this;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Add -value $sb -force;

	$code = '
	param($name)
	$temp = @();
	foreach($rege in $this.registry)
	{
		if($name -ne $rege.name)
		{
			$r = object-coderegistry -name $rege.name -value $rege.value;
			$temp += $r;
		}
	}
	$this | add-member -membertype noteproperty -name registry -value ($temp | sort name) -force;
	$this;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Remove -value $sb -force;

	$code = '
	param($name)
	$ret = $this.find($name).value;
	$ret;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name GetValue -value $sb -force;

	$ret;
}
