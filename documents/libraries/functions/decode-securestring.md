# Decode-SecureString
``` powershell
Decode-SecureString(
  [parameter(mandatory=$true)][System.Security.SecureString]$secstring
)
```
This function decodes a SecureString into a plain text.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| secstring | Yes | SecureString to decode. |

#### Returns
a plain text.

``` powershell
# input credential
$cred = Get-Credential;

# decode
Decode-SecureString $cred.Password;
```
