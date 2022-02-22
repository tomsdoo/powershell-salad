# Get-Account
``` powershell
Get-Account(
  [string]$sid,
  [string]$domain,
  [string]$username
)
```
This function gets an account information.

#### Parameters
only SID, or domain and user name should be inputted.

|name|necessary|description|
|:--|:--|:--|
| sid | No | SID string.(SDDL) |
| domain | No | domain string. |
| username | No | username string. |

#### Returns
an instance which is [SID] or [NTAccount].

``` powershell
# parameter error
Get-Account;

# get with SID
Get-Account -sid s-1-5-18;

# get with the domain name and the user name
Get-Account -domain $env:USERDOMAIN -username $env:USERNAME;
```
