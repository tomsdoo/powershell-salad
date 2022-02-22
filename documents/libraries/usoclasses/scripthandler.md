# ScriptHandler
This class provides the functions around the scripts.  
MSScriptControl.ScriptControl COM object is used internal so that SALAD session should be started on 32bit mode.  
If ScriptControl is not installed, you should install ScriptControl to use this class or inheritors of this class.  
It's not to be used directly. It should be that this class methods are used in the instances of inheritors.  
See [VBSHandler](vbshandler.md), [JSHandler](jshandler.md).

#### Properties
|name|description|
|:--|:--|
| AllScript | the string of all scripts that would be set in [ScriptHandler.Initialize]. |
| Language | the language line VBScript or some. |
| ScriptControl | an instance of COM object that is MSScriptControl.ScriptControl. |
| ScriptFileExtension | the file extension of the script. |
| ScriptFolder | the folder path that would be set in [ScriptHandler.Initialize]. |

***

#### Initialize($folder)
This method initializes the instance.

##### Parameters
|name|description|
|:--|:--|
| $folder | the folder path that the script files in it. |

##### Returns
this instance.

***

#### InitializeEx()
This method would be called by ScriptHandler.Initialize().  
This method should be overridden by the inheritors.

##### Parameters
nothing

##### Returns
nothing
