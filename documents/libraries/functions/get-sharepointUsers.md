# Get-SharePointUsers
``` powershell
Get-SharePointUsers(
  [parameter(mandatory=$true)][string]$SiteUrl,
  [switch]$UseDefaultCredential,
  [switch]$AllProperties
)
```
This function returns the information of the users in Sharepoint site.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| SiteUrl | Yes | target site url, like "http://myserver/". |
| UsedefaultCredential | No | means to fetch data with default credentials. |
| AllProperties | No | means not to select the properties to return. |

#### Returns
the information of the users.

``` powershell
# get the users
Get-SharePointUsers -SiteUrl http://myserver/;

# get the users with all properties
Get-SharePointUsers -SiteUrl http://myserver/ -AllProperties;

# get the users with default credentials
Get-SharePointUsers -SiteUrl http://myserver/ -UsedefaultCredential;
```
