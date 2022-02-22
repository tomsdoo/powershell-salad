# Encode-String
``` powershell
Encode-String(
  [parameter(mandatory=$true)][string]$string,
  [parameter(mandatory=$true)][string]$keystring
)
```
This function encodes a string.([Decode-String](decode-string.md) can decode)

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| string | Yes | plain text |
| keystring | Yes | key string |

*Returns*
a string encoded.

``` powershell
# encode
$r = Encode-String -string "P@ssw0rd!" -keystring "abcde";
# check the string out
$r;
# fail to decode
Decode-String -string $r -keystring "sdf";
# succeed to decode
Decode-String -string $r -keystring "abcde";
```
