# Get-SharePointListItems
``` powershell
Get-SharePointListItems(
  [parameter(mandatory=$true)][string]$SiteUrl,
  [parameter(mandatory=$true)][string]$ListName,
  [string]$ViewName,
  [switch]$WithAce,
  [switch]$WithJet,
  [switch]$UseDefaultCredential
)
```
This function gets the items of a Sharepoint list.  
It's required to choose WithAce or WithJet to fetch data.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| SiteUrl | Yes | target site url, like "http://myserver.domain/". |
| ListName | Yes | target list name, like "mylist". |
| ViewName | No | target view name, like "myview". |
| WithAce | No | means to fetch with AccessDatabaseEngine. To use this switch, it's required that ACE installed. |
| WithJet | No | means to fetch with JointEngineTechnology. To use this switch, it's required that JET installed. |
| UseDefaultCredential | No | means to fetch with default credentials. |

#### Returns
an array of the items.

``` powershell
# ACE
Get-SharePointListItems -SiteUrl http://myserver/ -ListName mylist -WithAce;

# JET
Get-SharePointListItems -SiteUrl http://myserver/ -ListName mylist -WithJet;

# use default credentials
Get-SharePointListItems -SiteUrl http://myserver/ -ListName mylist -WithAce -UseDefaultCredential;
```
