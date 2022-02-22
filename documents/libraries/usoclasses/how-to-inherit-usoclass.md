# How To Inherit USOClass
USOClasses can inherit other USOClasses.

Define the class name that the inheritor inherits from.
And write it in the inheritor's constructor code.

```[InheritorClassName].Constructor.code```
``` powershell
param()
$baselist = @();
$baselist += "[ClassNameOfInheritFrom]";

$this | add-member -membertype noteproperty -name BaseClass -value $baselist -force;
$this;
```

### For example
Now we make a class named MyAltTestClass that inherits MyTestClass.

```MyTestClass.Echo.code```
``` powershell
param($strname)
$ret = "This method is defined in MyTestClass";
$ret += "`n";
$ret += ("This class name is " + $this.classname);
$ret += "`n";
$ret += ("Value is " + $strname);
$ret;
```

```MyAltTestClass.constructor.code```
``` powershell
param()
$baselist = @();
$baselist += "MyTestClass";
$this | add-member -membertype noteproperty -name BaseClass -value $baselist -force;
$this;
```

In new session at SALAD, input below.
``` powershell
$myclass = new myalttestclass;
$myclass.echo("ABC");
```

Echo method in MyAltTestClass works, although no one defined in MyAltTestClass.
This is the inheritance.
