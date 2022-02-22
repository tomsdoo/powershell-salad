# Visibility Of Commands
The function named ```Reflect-Visibility``` sets the visibility of the commands and the functions.
The users can handle the visibility of the functions by the lines of follows.

``` powershell
#VISIBILITY:public
```
``` powershell
#VISIBILITY:private
```

Like this:
``` powershell
function global:MyPublicFunction()
#VISIBILITY:public
{
   Write-Host "This is public function";
}
```
``` powershell
function global:MyPrivateFunction()
#VISIBILITY:private
{
    Write-Host "This is not public function";
}
```

Reflect-Visibility reads all of the functions and seek the lines of ```#VISIBILITY:public```, and write public function names in ```public_func.txt```.  
```public_func.txt``` is in [SALADROOT.definition.system.publiccommand](saladroot.definition.system.publiccommand.md).
