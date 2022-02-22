# SALADROOT.definition.system
System definitions are placed in this folder.

#### Directories
|name|description|
|:--|:--|
| [administrator](saladroot.definition.system.administrator.md) | administrators in SALAD are defined in it. |
| directoryinfo | information of the directories made by Check-Environment function. |
| [publiccommand](saladroot.definition.system.publiccommand.md) | public commands in SALAD are defined in it.  |

#### To get this directory
``` powershell
$SessionManager.GetValue("system.session.directory.definition.system");
```
