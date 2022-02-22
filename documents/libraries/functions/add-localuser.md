# Add-LocalUser
```
Add-LocalUser(
  [string]$computername,
  [parameter(mandatory=$true)][string]$user,
  [parameter(mandatory=$true)][string]$password
)
```

This function adds a new local user to the target computer with ADSI.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | target computer. If it's not inputted, the target will be the local computer. |
| user | Yes | a local user name. |
| password | Yes | a password for the user. |

#### Returns
nothing.

ex)
``` powershell
# check the users before operation
Get-LocalUsers;

# operation
Add-LocalUser -user TestUser -password "P@ssw0rd";

# check the users after operation
Get-LocalUsers;

# for the remote computer
Get-LocalUsers -computername $env:computername;
Add-LocalUser -computername $env:computername -user TestUser -password "P@ssw0rd";
Get-LocalUsers -computername $env:computername;
```

See also [Get-LocalUsers](get-localusers.md).
