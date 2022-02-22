# NTAccount
This class represents an account.

#### Base class
[IdentifierBase](identifierbase.md)

#### Initialize($domain, $name)
This method initializes the members.

##### Parameters
|name|description|
|:--|:--|
| $domain | domain name. |
| $name | user name. |

##### Returns
this instance.

``` powershell
# create an instance
$n = new NTAccount;

# initialize
$n = $n.Initialize($env:USERDOMAIN, $env:USERNAME);

# check SID
$n.SID.Value;

# check the account
$n.NTAccount.Value;
```
