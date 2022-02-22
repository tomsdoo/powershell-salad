function global:Load-Classes()
#VISIBILITY:public
{
	$usingnamespacefilename = "class__.cs";
	$classroot = $SessionManager.GetValue("system.session.directory.classes");

	$namespacefolderl = Get-ChildItem $classroot | ?{$_.Attributes -like "*directory*"};
	$directivel = @();
	$codestr = [string]::Empty;
	$bgfl = 
	foreach($namespacefoldere in $namespacefolderl)
	{
		Get-ChildItem $namespacefoldere.FullName | ?{$_.Name -eq $usingnamespacefilename} | %{Get-Content $_.FullName} | %{$directivel += $_};
		Get-ChildItem $namespacefoldere.FullName | ?{$_.Extension -eq ".cs"} | ?{$_.Name -ne $usingnamespacefilename} | %{New-Object bgfilereader($_.FullName)};
	}
	while($true)
	{
		[System.Threading.Thread]::Sleep(10);
		if(($bgfl | ?{$_.busy}) -eq $null)
		{
			break;
		}
	}
	$sb = New-Object System.Text.StringBuilder;
	$sb.Append("namespace SALAD{" + ([System.Environment]::NewLine)) | Out-Null;
	foreach($bgfe in $bgfl)
	{
		foreach($linee in $bgfe.resultlines)
		{
			$sb.Append($linee + ([System.Environment]::NewLine)) | Out-Null;
		}
	}
	$sb.Append("}" + ([System.Environment]::NewLine)) | Out-Null;
	$codestr = $sb.ToString();

	$directivestr = [string]::Empty;
	$directivel | Group-Object | %{$_.Name.ToString()} | %{$directivestr += ($_ + ([System.Environment]::NewLine))};

	$allstr = [string]::Empty;
	$allsb = New-Object System.Text.StringBuilder;
	$allsb.Append($directivestr) | Out-Null;
	$allsb.Append($codestr) | Out-Null;
	$allstr = $allsb.ToString();

	## ReferencedAssemblies
	$ral = @();
	$ral += "System.Core";
	$ral += "System.Configuration";
	$ral += "System.Data";
	$ral += "System.Drawing";
	$ral += "System.Web";
	$ral += "System.Security";
	$ral += "System.Xml";
	$ral += "System.Windows.Forms";
	$ral += "System.Management";
	$ral += "System.DirectoryServices";

	## Read class-Definitions
	Add-Type -TypeDefinition $allstr -referencedassemblies $ral;
}
