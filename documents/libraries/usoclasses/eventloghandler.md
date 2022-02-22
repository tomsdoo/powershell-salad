# EventLogHandler
This class provided the functions about event logs.

#### Methods
- GetLog]($logname)
- Initialize]($computername)

#### Initialize($computername)
This method initializes the class.

##### Parameters
|name|description|
|:--|:--|
| $computername | target computer name. |

##### Returns
this instance.


#### GetLog($logname)
This method gets eventlog data.
The parameter as $logname is a string following.
- system
- application
- security

##### Returns
an array of the logs.

***
#### Examples
``` powershell
# create an instance
$e = new EventLogHandler;
# initialize
$e = $e.Initialize($env:computername);
# get the logs
$e.GetLog("system");
```
