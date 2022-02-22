# Get-TraceLog
``` powershell
Get-TraceLog(
  [string]$user,
  [string]$datestr
)
```
This function gets the trace log.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| user | No | the user name. if not inputted, all users are selected. |
| datestr | No | date string formatted yyyyMMdd. if not inputted, all days are selected. |

#### Returns
an array of the trace log.
See TraceLogCollector for the trace log data format.

``` powershell
# get all
Get-TraceLog;

# get own log
Get-TraceLog -user $env:USERNAME;

# get own logs what are outputted today
$l = Get-TraceLog -user $env:USERNAME -datestr (Get-Date -Format "yyyyMMdd");
# and select lines that are like get*
$l | ?{$_.commandline -like "get*"};
```
