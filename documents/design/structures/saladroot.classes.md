# SALADROOT.classes
C# code files are placed in this folder.

The namespace folders are placed in classes folder.
The environment is to import C# code files.
The namespace folder has one file named ```class__.cs``` that the using-directives are written in.
Class definitions are written in other files named ```class_[whatever].cs``` like ```class_myclass.cs```, that the contents are without using-directives.

#### To get this directory
``` powershell
$SessionManager.GetValue("system.session.directory.classes");
```
