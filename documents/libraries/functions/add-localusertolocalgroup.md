# Add-LocalUserToLocalGroup
```
Add-LocalUserToLocalGroup(
  [string]$computername,
  [parameter(mandatory=$true)][string]$group,
  [parameter(mandatory=$true)][string]$user
)
```
This function adds a local user into a local group with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| group | Yes | target group name that is a local group. |
| user | Yes | local user name. |

#### Returns
nothing.

``` powershell
# check the members before
Get-LocalGroupMembers -group Administrators;

# operation
Add-LocalUserToLocalGroup -group Administrators -user TestUser;

# check the members after
Get-LocalGroupMembers -group Administrators;

# for remote operation
Get-LocalGroupMembers -computername $env:computername -group Administrators;
Add-LocalUserToLocalGroup -computername $env:computername -group Administrators -user TestUser;
Get-LocalGroupMembers -computername $env:computername -group Administrators;
```

See also [Get-LocalGroupMembers](get-localgroupmembers.md).
