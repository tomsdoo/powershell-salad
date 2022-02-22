# SessionManager
SessionManager manages [SessionRegistry](core.sessionregistry.md) and provides the definitions to the Functions and USOClasses.
Basically, the users refer the variables and definitions through this.

For example, the users can get the log folder by follows.
``` powershell
$SessionManager.GetValue("system.session.directory.log");
```

The users should use this because all of the informations are handled by this.

#### To get all names and values
``` powershell
$SessionManager.registry;
```
