# About Logging
The operation logs would be outputted by Transcript.
The administrators input below when they check the logs of the users.

``` powershell
# get trace log
Get-TraceLog;

#see directory
$logd = $SessionManager.GetValue("system.session.directory.log");

#explorer
explorer $logd;

#get-childitem or whatever
get-childitem $logd;
```
