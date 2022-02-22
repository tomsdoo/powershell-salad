# EnDecode
This class provides the functions about encryption.

#### Properties
Key : array of integer, 24 length.

*Methods*
- Decode($string)
- Encode($string)
- FileDecode($inputfilename, $outputfilename)
- FileEncode($inputfilename, $outputfilename)
- Initialize($keystring)


#### Decode($string)
This method decodes a string, returns a string decoded.

##### Parameters
|name|description|
|:--|:--|
| $string | encoded string |

##### Returns
a string decoded.


#### Encode($string)
This method encodes a string, returns a string encoded.

##### Parameters
|name|description|
|:--|:--|
| $string | plain text |

##### Returns
a string encoded.


#### FileDecode($inputfilename, $outputfilename)
This method decodes a text file.

##### Parameters
|name|description|
|:--|:--|
| $inputfilename | file encoded |
| $outputfilename | file decoded to output |

##### Returns
nothing

#### FileEncode($inputfilename, $outputfilename)
This method encodes a text file.

##### Parameters
|name|description|
|:--|:--|
| $inputfilename | file to encode, plain text file |
| $outputfilename | file to output |

##### Returns
nothing


#### Initialize($keystring)
This method sets the property of the instance. Key property will be set.

#### Examples
``` powershell
# new instance
$encoder = new EnDecode;

# initialize with key string
$encoder = $encoder.Initialize("abcde");

# encode
$encodedstring = $encoder.Encode("P@ssw0rd!");

# new instance for decoding
$decoder = new EnDecode;

# initialize with incorrect key string
$decoder = $decoder.Initialize("asdf");

# fail to decode
$decoder.Decode($encodedstring);

# initialize with correct key string
$decoder = $decoder.Initialize("abcde");

# succeed to decode
$decoder.Decode($encodedstring);
```

``` powershell
# new instance
$encoder = new EnDecode;

# initialize with key string
$encoder = $encoder.Initialize("abcde");

# encode
$encoder.FileEncode("c:\temp\test.txt", "c:\temp\test.txt.txt");

# new instance
$decoder = new EnDecode;

# initialize with incorrect key string
$decoder = $decoder.Initialize("abc");

# fail to decode
$decoder.FileDecode("c:\temp\test.txt.txt", "c:\temp\test.txt.txt.txt");

# initialize with correct key string
$decoder = $decoder.Initialize("abcde");

# succeed to decode
$decoder.FileDecode("c:\temp\test.txt.txt", "c:\temp\test.txt.txt.txt");
```
