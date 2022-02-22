# ADClient
This class provides the functions to access Active Directory.

#### Methods
- Search($keyword)
- TranslateToSecurityIdentifier($DirectoryEntry)



##### Search($keyword)
Search from Active Directory with Name/DisplayName/SID(SDDL).

###### Parameters
|name|description|
|:--|:--|
| $keyword | a string of cn, displayname or SID. |

###### Returns
an instance of System.DirectoryServices.DirectoryEntry.

##### TranslateToSecurityIdentifier($DirectoryEntry)
Translate DirectoryEntry to SID(SDDL).

###### Parameters
|name|description|
|:--|:--|
| $DirectoryEntry | an instance of System.DirectoryServices.DirectoryEntry. |

###### Returns
an instance of System.Security.Principal.SecurityIdentifier.

#### example
``` powershell
# create an instance
$adc = new ADClient;

# search
$myde = $adc.Search($env:username);

# check the result
$myde;

# translate to SID
$adc.TranslateToSecurityIdentifier($myde);
```
