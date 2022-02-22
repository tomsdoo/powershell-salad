# Set-LocalUserPassword
``` powershell
Set-LocalUserPassword(
  [string]$computername,
  [parameter(mandatory=$true)][string]$user,
  [parameter(mandatory=$true)][string]$password
)
```
This function sets a password for a local user with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| user | Yes | local user. |
| password | Yes | password. |

#### Returns
nothing.

``` powershell
Set-LocalUserPassword -user TestUser -password "P@ssw0rd!";

# for remote operation
Set-LocalUserPassword -computername $env:computername -user TestUser -password "P@ssw0rd!";
```
