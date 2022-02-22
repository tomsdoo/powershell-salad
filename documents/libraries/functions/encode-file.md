# Encode-File
``` powershell
Encode-File(
  [parameter(mandatory=$true)][string]$inputfilename,
  [parameter(mandatory=$true)][string]$outputfilename,
  [parameter(mandatory=$true)][string]$keystring
)
```
This function encodes a text file.([Decode-File](decode-file.md) can decode the encoded file)

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| inputfilename | Yes | text file |
| outputfilename | Yes | encoded file to output |
| keystring | Yes | key string for decoding |

#### Returns
nothing

``` powershell
# encode file
Encode-File -inputfilename C:\temp\test.txt -outputfilename C:\temp\test.txt.txt -keystring "abcde";
# fail to decode (incorrect keystring)
Decode-File -inputfilename C:\temp\test.txt.txt -outputfilename C:\temp\test.txt.txt.txt -keystring "abcd";
# succeed to decode
Decode-File -inputfilename C:\temp\test.txt.txt -outputfilename C:\temp\test.txt.txt.txt -keystring "abcde";
```
