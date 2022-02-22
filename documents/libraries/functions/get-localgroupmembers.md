# Get-LocalGroupMembers
``` powershell
Get-LocalGroupMembers(
  [string]$computername,
  [parameter(mandatory=$true)][string]$group
)
```
This function gets the members of the local group with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| group | Yes | group name. |

#### Returns
an array of member names.

``` powershell
Get-LocalGroupMembers -group Administrators;

# for remote operation
Get-LocalGroupMembers -computername $env:computername -group Administrators;
```
