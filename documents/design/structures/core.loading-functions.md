# Loading Functions
The function files are to be placed in [SALADROOT.scripts](saladroot.scripts.md) and the environment is to import all the functions.
One function is written in one file, one file has one function.
File name is equal to the function name.

ex) filename: ```ROOT\scripts\wrapunits\Get-MyFunc.ps1```

``` powershell
function global:Get-MyFunc()
#VISIBILITY:public
{
    Write-Host "this is my function.";
}
```
