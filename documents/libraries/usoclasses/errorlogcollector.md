# ErrorLogCollector
This class handles the error logs of USOClasses.  
The methods of USOClasses have a try-catch code as default.
Default try-catch lines output the error logs in SALAD log folder.

#### Methods
- GetLog]($keyword)
- Initialize]($userid)

#### Error information format
|name|description|
|:--|:--|
| FileName | a file name that error is written in. |
| Date | date string formatted yyyyMMdd. |
| OnWhat | a computer name where error occured. |
| Who | a user name who met the error. |
| WhatKind | invinfo = invocation info / recinfo = error record |
| DateTimeDetail | date time string formatted yyyyMMddHHmmssfffff. |
| LogData | detail of error information. |

#### Initialize($userid)
This method initializes the class.

##### Parameters
|name|description|
|:--|:--|
| $userid | the user name of the target. |

##### Returns
this instance.

#### GetLog($keyword)
This method reads the error logs.  
$keyword is a keyword for the log file name, and it is an optional parameter.

##### parameters
|name|description|
|:--|:--|
| $keyword | date time string formatted yyyyMMddHHmmss. |

##### Returns
an array of error logs.

***

#### Examples

``` powershell
# create instance
$e = new ErrorLogCollector;

# initialize
$e = $e.Initialize($env:username);

# get errors that occured today
$dtstr = Get-Date -Format "yyyyMMdd";
$errorlist = $e.GetLog($dtstr);

# check all
$errorlist;

# get the errors that occured on this computer
$mycomputererrors = $errorlist | ?{$_.OnWhat -eq $env:computername};

# show details
$mycomputererrors | %{$_.LogData};
```
