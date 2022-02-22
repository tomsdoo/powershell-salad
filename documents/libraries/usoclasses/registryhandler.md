# RegistryHandler
This class provides the functions around Windows registries.

#### Properties
|name|description|
|:--|:--|
| ComputerName | this property has the target computer name. |
| HiveNames | hive names. HKEY_LOCAL_MACHINE / HKEY_CURRENT_USER / HKEY_CLASSES_ROOT / HKEY_CURRENT_CONFIG / HKEY_DYN_DATA / HKEY_PERFORMANCE_DATA / HKEY_USERS |
| ValueKinds | value kinds. BINARY / DWORD / EXPANDSTRING / MULTISTRING / STRING |

#### Methods
- Initialize($computername)
- GetSubKeyNames($hive, $path) : string[]
- GetTree($hive, $path) : {name, value}[]
- GetValue($hive, $path, $name) : any
- GetValueNames($hive, $path) : string[]
- OpenBaseSubKey($hive, $path, $writing) : RegistryHandler
- SetValue($hive, $path, $name, $value, $valuekind) : void
- CloseBaseSubKey() : void

#### Examples
``` powershell
# create instance
$rh = new RegistryHandler;

# initialize
$rh = $rh.Initialize($env:COMPUTERNAME);

# get sub key names
$subkeys = $rh.GetSubKeyNames($rh.HiveNames.HKEY_LOCAL_MACHINE, "software");

# check it out
$subkeys;

# get tree
$t = $rh.GetTree($rh.HiveNames.HKEY_LOCAL_MACHINE, "software\microsoft\windows nt\currentversion\winlogon");

# check it out
$t;

# get value
$myvalue = $rh.GetValue($rh.HiveNames.HKEY_LOCAL_MACHINE, "software\microsoft\windows nt\currentversion\winlogon", "shell");

# check it out
$myvalue;
```
