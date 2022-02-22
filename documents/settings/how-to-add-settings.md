# How To Add Settings
Add settings of [SessionManager].

### Find a file where settings are
the directory that you get by input below.
find out [SALADROOT.bootstrap].
``` powershell
$SessionManager.GetValue("system.session.directory.bootstrap");
```

The folder has a file named ```initialize_userenv.ps1```.
And this file has the settings of user-environment, not system-environment.
So you can modify [SessionRegistry].

### initialize_userenv.ps1
To add global definition, write like this.
``` powershell
param()
$global:SessionManager = $SessionManager.Add("user.url.google", "http://www.google.com/");
```

For example, adding that "MyName" as the name and UserName as the value.
``` powershell
param()
$global:SessionManager = $SessionManager.Add("user.url.google", "http://www.google.com/");
$global:SessionManager = $SessionManager.Add("MyName", $ENV:UserName);
```

The users can use it like this, at next session.
And tab expansion works too.
``` powershell
$SessionManagger.GetValue("MyName");
```
