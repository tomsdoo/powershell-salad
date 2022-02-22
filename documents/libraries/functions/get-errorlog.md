# Get-ErrorLog
``` powershell
Get-ErrorLog(
  [string]$user,
  [string]$datetimestr
)
```
This function gets the error logs of USOClasses.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| user | No | user name who met the errors. |
| datetimestr | No | date time when the error occured, formatted yyyyMMddHHmmss. |

#### Returns
an array of error information. See [ErrorLogCollector] for detail.

``` powershell
# get errors
$el = Get-ErrorLog;
# check all errors
$el;

# get own errors
$el = Get-ErrorLog -user $env:username;

# get own errors and that errors occured today
$el = Get-ErrorLog -user $env:username -datetimestr (Get-Date -Format "yyyyMMdd");

# check the errors what occured on this computer
$el  = Get-ErrorLog;
$mycomputererrors = $el | ?{$_.OnWhat -eq $env:computername};

# and check details
$mycomputererrors | %{$_.LogData};
```
