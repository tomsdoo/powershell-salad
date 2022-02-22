# SID
This class represents SID.

#### Base class
[IdentifierBase](identifierbase.md)

#### Initialize($sddlsid)
This method initializes the members.

##### Parameters
|name|description|
|:--|:--|
| $sddlsid | string of SID |

##### Returns
this instance.

``` powershell
# create an instance
$s = new SID;

# initialize
$s = $s.Initialize("S-1-5-18");

# check the account
$s.NTAccount.Value;

# check SID
$s.SID.Value;
```
