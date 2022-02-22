# Get-Registry
``` powershell
Get-Registry(
  [string]$computername,
  [switch]$HKEY_CLASSES_ROOT,
  [switch]$HKEY_LOCAL_MACHINE,
  [switch]$HKEY_USERS,
  [parameter(mandatory=$true)][string]$path,
  [string]$name,
  [switch]$tree
)
```
This function gets registry.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| computername | No | remote computer name. |
| HKEY_CLASSES_ROOT | No | hive chosen. only one hive should be chosen. |
| HKEY_LOCAL_MACHINE | No | hive chosen. only one hive should be chosen. |
| HKEY_USERS | No | hive chosen. only one hive should be chosen. |
| path | Yes | registry path. |
| name | No | registry name. tree or name should be input. |
| tree | No | to get list of path and name and value. tree or name should be input. |

#### Returns
value or list.

``` powershell
# get list
$r = Get-Registry -HKEY_LOCAL_MACHINE -path "software\microsoft\windows nt\currentversion\winlogon" -tree;

# check the result out
$r;

# get value
$r = Get-Registry -HKEY_LOCAL_MACHINE -path "software\microsoft\windows nt\currentversion\winlogon" -name "shell";

# check
$r;
```
