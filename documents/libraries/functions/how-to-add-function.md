# How to add a function

### Find the directory the functions are
Get the directory by inputting following line.
``` powershell
# at SALAD env
$SessionManager.GetValue("system.session.directory.scripts");
```

To see the folder with explorer directory, following line.
``` powershell
# at SALAD env
explorer $SessionManager.GetValue("system.session.directory.scripts");
```

### Make a function file
for example: function named Echo-Info

``` powershell:Echo-Info.ps1
function global:Echo-Info()
#VISIBILITY:public
{
    $ret = [string]::Empty;
    $ret += $env:username;
    $ret += " on ";
    $ret += $env:computername;
    $ret;
}
```

### Reflect the function
Put the file in [SALADROOT.scripts].
To reflect visibility of the functions, input below.

``` powershell
# at SALAD env
Reflect-Visibility;
```

And the users can input Echo-Info in SALAD to get the name and the computer next session.
``` powershell
# at SALAD env
Echo-Info;
```
