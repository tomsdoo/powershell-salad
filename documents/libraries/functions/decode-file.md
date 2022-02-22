# Decode-File
``` powershell
Decode-File(
  [parameter(mandatory=$true)][string]$inputfilename,
  [parameter(mandatory=$true)][string]$outputfilename,
  [parameter(mandatory=$true)][string]$keystring
)
```
This function decodes a file that is made by using [Encode-File](encode-file.md).
See [Encode-File](encode-file.md) for example.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| inputfilename | Yes | encoded file |
| outputfilename | Yes | decoded file to output |
| keystring | Yes | key string for decoding |

#### Returns
nothing
