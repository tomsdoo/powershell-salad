# SALADROOT
SALAD root is a directory that has all resources of SALAD.

#### directories
|name|description|
|:--|:--|
|[bootstrap](saladroot.bootstrap.md)|Initializing scripts are in it.|
|[classes](saladroot.classes.md)|C# code files are in it.|
|[definition](saladroot.definition.md)|USOClasses code files and some definitions are in it.|
|[design](saladroot.design.md)|Whatever documents are in it.|
|[log](saladroot.log.md)|Logs are in it.|
|[scripts](saladroot.scripts.md)|[Functions] are in it.|
|[temporary](saladroot.temporary.md)|Whatever files are in it.|

#### files
|name|description|
|:--|:--|
|```start_interactive.bat```|a file to start SALAD.|
|```start_interactiveWOW.bat```|a file to start SALAD on WOW64.|


#### To get this directory
``` powershell
$SessionManager.GetValue("system.session.directory.root");
```
