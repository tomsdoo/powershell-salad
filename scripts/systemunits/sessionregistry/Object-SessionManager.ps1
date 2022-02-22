function global:Object-SessionManager([parameter(mandatory=$true)][string]$filename, [switch]$admin)
#VISIBILITY:public
{
	$temp = $null;
	if(test-path $filename)
	{
		$temp = import-clixml $filename;
	}
	if(!$temp)
	{
		$temp = @();
	}

	$ret = new-object psobject;
	$ret | add-member -membertype noteproperty -name registry -value $temp -force;
	$ret | add-member -membertype noteproperty -name filename -value $filename -force;

	$code = '
	param($name)
	$this.registry | ?{$_.name -eq $name};
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Find -value $sb -force;

	$code = '
	param($name, $value)
	$r = object-sessionregistry -name $name -value $value;
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
			$r = object-sessionregistry -name $rege.name -value $rege.value;
			$temp += $r;
		}
	}
	$this | add-member -membertype noteproperty -name registry -value ($temp | sort name) -force;
	$this;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Remove -value $sb -force;

	$code = '
	param($notexpandedvalue)
	$ret = $notexpandedvalue;
	while($true)
	{
		if(!($ret -like "*@_*_@*"))
		{
			break;
		}
		foreach($rege in $this.registry)
		{
			$r = object-sessionregistry -name $rege.name -value $rege.value;
			$ret = $ret -replace $r.replacecandidate(), $r.value;
		}
	}
	$ret;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Expand -value $sb -force;

	$code = '
	param($name)
	$ret = $this.Expand($this.find($name).value);
	$ret;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name GetValue -value $sb -force;

	$code = '
	param($reportfilename)
	$this.registry | export-csv $reportfilename -encoding default;
	'
	$sb = [scriptblock]::create($code);
	$ret | add-member -membertype scriptmethod -name Report -value $sb -force;

	if($admin)
	{
		$code = '
		param()
		$this.registry | export-clixml $this.filename;
		'
		$sb = [scriptblock]::create($code);
		$ret | add-member -membertype scriptmethod -name Save -value $sb -force;
	}

	$ret;
}
