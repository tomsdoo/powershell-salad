function global:Generate-Life([parameter(mandatory=$true)][int]$row, [parameter(mandatory=$true)][int]$column, [parameter(mandatory=$true)][int]$generation, $deadchar, $alivechar)
#VISIBILITY:public
{
	$dc = "-";
	$ac = "+";
	if($deadchar -ne $null)
	{
		$dc = $deadchar;
	}
	if($alivechar -ne $null)
	{
		$ac = $alivechar;
	}
	$t = New-Object SALAD.Life.TableClass($row, $column);
	$t.SetRandom();
	for($i = 0; $i -lt $generation; $i++)
	{
		[System.Threading.Thread]::Sleep(200);
		Clear-Host;
		$t.Display($dc, $ac);
		$t.Next();
	}
}
