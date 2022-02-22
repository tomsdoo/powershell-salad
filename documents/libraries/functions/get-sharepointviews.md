# Get-SharePointViews
``` powershell
Get-SharePointListViews(
  [parameter(mandatory=$true)][string]$SiteUrl,
  [parameter(mandatory=$true)][string]$ListName,
  [switch]$UseDefaultCredential,
  [switch]$AllProperties
)
```
This function returns the view information of a Sharepoint list.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| SiteUrl | Yes | target site url, like "http://myserver/". |
| ListName | Yes | target list name, like "mylist". |
| UseDefaultCredential | No | means to fetch with default credentials. |
| AllProperties | No | means not to select the properties to return. |

#### Returns
the information of the view of the list.

``` powershell
# get the views
Get-SharePointListViews -SiteUrl http://myserver/ -ListName mylist;

# get the views with all properties
Get-SharePointListViews -SiteUrl http://myserver/ -ListName mylist -AllProperties;

# get the views with default credentials
Get-SharePointListViews -SiteUrl http://myserver/ -ListName mylist -UseDefaultCredential;
```
