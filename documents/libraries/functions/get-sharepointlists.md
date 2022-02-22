# Get-SharePointLists
``` powershell
Get-SharePointLists(
  [parameter(mandatory=$true)][string]$SiteUrl,
  [switch]$UseDefaultCredential,
  [switch]$AllProperties
)
```
This function returns Sharepoint list information.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| SiteUrl | Yes | target site url, like "http://myserver/". |
| UseDefaultCredential | No | means to fetch with default credentials. |
| AllProperties | No | means not to select properties to return. |

#### Returns
the information of the Sharepoint lists.

``` powershell
# get the lists
Get-SharePointLists -SiteUrl http://myserver/;

# get the lists with all properties
Get-SharePointLists -SiteUrl http://myserver/ -AllProperties;

# get the lists with default credentials
Get-SharePointLists -SiteUrl http://myserver/ -AllProperties -UseDefaultCredential;
```
