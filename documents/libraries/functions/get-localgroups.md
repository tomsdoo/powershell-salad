# Get-LocalGroups
``` powershell
Get-LocalGroups(
  [string]$computername
)
```
This function gets the local group names with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |

#### Returns
an array of group names.

``` powershell
Get-LocalGroups;

# for remote operation
Get-LocalGroups -computername $env:computername;
```
