# Get-LocalUserFlags
``` powershell
Get-LocalUserFlags(
  [string]$computername,
  [parameter(mandatory=$true)][string]$user
)
```
This function gets the flags of the user with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| user | Yes | local user. |

#### Returns
an object that has the properties of the flags.

``` powershell
Get-LocalUserFlags -user TestUser;

# for remote operation
Get-LocalUserFlags -computername $env:computername -user TestUser;
```
