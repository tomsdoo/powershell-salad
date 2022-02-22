# SALADROOT.bootstrap
The initializing script files are placed in this folder.
The files in this folder called specifically.

#### Files
|name| description|
|:--|:--|
|dummyprompt.ps1| A prompt function file what's in use while SALAD's initializing. |
|initialize_assemblyenv.ps1| An initializing script that registers the information of assemblies into SessionRegistry. It's called by ```initialize_psenv.ps1```. |
|initialize_psenv.ps1| An initializing script. |
|initialize_saladassemblyenv.ps1| An initializing script that registers the information of SALAD assemblies into SessionRegistry. It's called by ```initialize_psenv.ps1```. |
|initialize_sharputil.ps1| An initializing script that gets ready to use some utilities. It's called by ```initialize_psenv.ps1```. |
|initialize_userenv.ps1| An initializing script for user-environments.  It's called by ```initialize_psenv.ps1```. Admins can feel free to define some global definitions in SALAD of their own environments. |
| TabExpansion.ps1 | A customized TabExpansion. It provides easiness to customize tab-expansion. |

#### To get this directory
``` powershell
$SessionManager.GetValue("system.session.directory.bootstrap");
```
