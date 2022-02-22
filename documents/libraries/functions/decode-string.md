# Decode-String
``` powershell
Decode-String(
  [parameter(mandatory=$true)][string]$string,
  [parameter(mandatory=$true)][string]$keystring
)
```
This function decodes a string encoded by using [Encode-String](encode-string.md).
See [Encode-String](encode-string.md) for example.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| string | Yes | a string encoded |
| keystring | Yes | key string |

#### Returns
a string decoded.
