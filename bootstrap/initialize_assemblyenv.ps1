param()

$strl = @();
$strl += "DirectoryServices";
$strl += "DirectoryServices.DirectoryEntry";
$strl += "DirectoryServices.DirectorySearcher";

$strl += "Microsoft.Office.Interop";
$strl += "Microsoft.Office.Interop.Excel";
$strl += "Microsoft.Office.Interop.Outlook";
$strl += "Microsoft.Office.Interop.Word";

$strl += "System.Data.OleDb.OleDbConnection";
$strl += "System.Data.OleDb.OleDbCommand";

$strl += "System.Data.SqlClient.SqlCommand";
$strl += "System.Data.SqlClient.SqlConnection";

$strl += "System.DirectoryServices";
$strl += "System.DirectoryServices.DirectoryEntry";
$strl += "System.DirectoryServices.DirectorySearcher";

$strl += "System.Security.Principal.SecurityIdentifier";

$strl += "System.Text.StringBuilder";

$strl += "[Microsoft.Win32.RegistryHive]";
$strl += "[Microsoft.Win32.RegistryKey]";
$strl += "[System.Reflection.Assembly]";

foreach($stre in $strl)
{
	$global:SessionManager = $SessionManager.Add($stre, $stre);
}
