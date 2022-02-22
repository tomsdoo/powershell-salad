# Remove-LocalUser
``` powershell
Remove-LocalUser(
  [string]$computername,
  [parameter(mandatory=$true)][string]$user
)
```
This function removes a local user with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| user | Yes | local user. |

#### Returns
nothing.

``` powershell
# check the users before operation
Get-LocalUsers;

# operation
Remove-LocalUser -user TestUser;

# check the users after operation
Get-LocalUsers;

# for remote operation
Get-LocalUsers -computername $env:computername;
Remove-LocalUser -computername $env:computername -user TestUser;
Get-LocalUsers -computername $env:computername;
```

See also [Get-LocalUsers](get-localusers.md).
