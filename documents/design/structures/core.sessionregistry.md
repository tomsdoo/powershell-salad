# SessionRegistry
A list of the definition sets that are made of Name and Value.

#### To get
``` powershell
$SessionManager.registry;

# see in text file
$SessionManager.registry | Out-Text;

# see in list formatted text
$SessionManager.registry | Format-List | Out-Text;
```

|name|description|
|:--|:--|
| Name | It's a key for value to get. |
| Value | It's a value made of whatever string. Tab expansion works for it. |

#### Expansion
Expansion works recursively for values.

##### Expanding expression
The names already exists in SessionRegistry between ```@_``` and ```_@``` in values will be expanded.
```
@_[WhateverNameAlreadyExists]_@
```

For example, try to define the user name, the computer name and the user name + the computer name.

``` powershell
#define user name
$SessionManager.Add("myusername", $ENV:USERNAME);

#define computer name
$SessionManager.Add("mycomputername", $ENV:COMPUTERNAME);

#define two of them
$SessionManager.Add("WhereAmI", "@_myusername_@ on @_mycomputername_@");

#get
$SessionManager.GetValue("WhereAmI");
```
