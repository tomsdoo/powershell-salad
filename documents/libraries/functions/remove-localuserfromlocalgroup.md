# Remove-LocalUserFromLocalGroup
``` powershell
Remove-LocalUserFromLocalGroup(
  [string]$computername,
  [parameter(mandatory=$true)][string]$group,
  [parameter(mandatory=$true)][string]$user
)
```
This function removes a local user from a local group with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| group | Yes | local group. |
| user | Yes | local user. |

#### Returns
nothing.

``` powershell
# check the members before operation
Get-LocalGroupMembers -group Administrators;

# operation
Remove-LocalUserFromLocalGroup -group Administrators -user TestUser;

# check the members after operation
Get-LocalGroupMembers -group Administrators;

# for remote operation
Get-LocalGroupMembers -computername $env:computername -group Administrators;
Remove-LocalUserFromLocalGroup -computername $env:compputername -group Administrators -user TestUser;
Get-LocalGroupMembers -computername $env:computername -group Administrators;
```

See also [Get-LocalGroupMembers](get-localgroupmembers.md).
