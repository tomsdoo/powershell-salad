# Get-LocalUsers
``` powershell
Get-LocalUsers([string]$computername)
```
This function gets the local users with ADSI.

#### Parameters
|name|necesary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |

#### Returns
an array of the local user names.

``` poershell
Get-LocalUsers;

# for remote operation
Get-LocalUsers -computername $env:computername;
```
