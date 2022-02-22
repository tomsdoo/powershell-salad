# VBSHandler
This class provides the functions around VBScript.

#### Base class
[ScriptHandler](scripthandler.md)

#### Properties
|name|description|
|:--|:--|
| Classes | the classes that are written in VBScript. |

***
#### InitializeEx()
This method initializes the instance.  
This method overrides ScriptHandler.InitializeEx().

#####Parameters
nothing

##### Returns
nothing

***

#### New($classname)
This method creates an instance of $classname.

##### Parameters
|name|description|
|:--|:--|
| $classname | the name of the class. |

##### Returns
an instance of the class.

***
#### Example
``` powershell
# create instance
$bh = new VBSHandler;

# initialize
# *.vbs files will be loaded
$bh = $bh.Initialize("C:\temp\vbs");

# create an instance that is written in VBScript
$myclass = $bh.New("MyClass");

# call method
$myclass.WhateverMethod($WhateverParameter);
```
