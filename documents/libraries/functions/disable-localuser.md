# Disable-LocalUser
``` powershell
Disable-LocalUser(
  [string]$computername,
  [parameter(mandatory=$true)][string]$user
)
```
This function disables a local user with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| user | Yes | local user. |

#### Returns
nothing.

``` powershell
# check the flags before
Get-LocalUserFlags -user TestUser;
# operation
Disable-LocalUser -user TestUser;
# check the flags after
Get-LocalUserFlags -user TestUser;

# for remote operation
Get-LocalUserFlags -computername $env:computername -user TestUser;
Disable-LocalUser -computername $env:computername -user TestUser;
Get-LocalUserFlags -computername $env:computername -user TestUser;
```

See also [Get-LocalUserFlags].
