# How To Add A USOClasses
You can make USOClasses.

### Make method file
make a text file with text editor named ```[ClassName].[MethodName].code```.

### Define method parameter
write the parameters for the first line like below.
``` powershell
param($strname)
```

### Define method process
write the process in PowerShell for the second or more after lines

#### For example
```MyTestClass.Echo.code```
``` powershell
param($strname)
$ret = "This method is defined in " + $this.classname;
$ret += "`n";
$ret += ("Value is " + $strname);
$ret;
```

### Reflect USO class
Put the class file to the directory.
The directory is got by inputting followwing line in SALAD.
``` powershell
$SessionManager.GetValue("system.session.directory.definition.code");
```

### Use it
When the USO class file is placed, we can use it next session.
``` powershell
$myclass = new mytestclass;
$myclass.echo("ABC");
```
