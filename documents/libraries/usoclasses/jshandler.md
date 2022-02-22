# JSHandler
This class provides the functions around JavaScript.

#### Base class
[VBSHandler](vbshandler.md)

#### InitializeEx()
This method initializes the instance.  
This method overrides VBSHandler.InitializeEx().

##### Parameters
nothing

##### Returns
nothing

***
``` powershell
# create instance
$jh = new JSHandler;

# initialize
# *.js files will be loaded
$jh = $jh.Initialize("c:\temp\js");

# create an instance that is written in JavaScript
$myclass = $jh.New("myclass");

# call method
$myclass.WhateverMethod($WhateverParameter);
```
