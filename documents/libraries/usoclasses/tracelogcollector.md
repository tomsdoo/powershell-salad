# TraceLogCollector
This class collects the trace log of SALAD.

### Methods
- GetLog($datestr)
- Initialize($user)

### Trace information format
| name | description |
|:--|:--|
| FileName | a file name of Transcrpit log. |
| DateStr | a date string. |
| OnWhat | the computer name. |
| Who | the user name. |
| When | operation date time as System.DateTime. |
| CommandLine | a line inputted. |

### Examples
``` powershell
# create instance
$tc = new TraceLogCollector;
# initialize
$tc = $tc.Initialize($env:username);
# get log
$l = $tc.GetLog((Get-Date -Format "yyyyMMdd"));
# check all
$l;
# check the log on this computer
$l | ?{$_.OnWhat -eq $env:computername};
```

#### GetLog($datestr)
This method gets the trace log.

##### Parameters
| name | description |
|:--|:--|
| $datestr | date string formatted yyyyMMdd. not necessary to input. |

##### Returns
an array of the trace log information.

#### Initialize($user)
This method initializes the instance.

##### Parameters
| name | description |
|:--|:--|
| $user | user name of the target. not necessary to input. |

##### Returns
this instance.
