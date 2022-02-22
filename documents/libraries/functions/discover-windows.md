# Discover-Windows
``` powershell
Discover-Windows($name)
```
This function discovers the computers that run Windows.
It will try to ping, execute WMI query, read the registry keys.
- It's expected that the pipeline would be used.
- The credential of the current user will be used in trying processes.

``` powershell
# this line will discover your computer.
$ENV:COMPUTERNAME | Discover-Windows;

# these lines will discover the IP addresses of your neighbors.
# Get IP addresses
$a = New-Object SALAD.TCP.MyIPEnvClass;

# Discover
$rl = $a.friendipl | Discover-Windows;

# Out into a text file
$rl | out-text;

# Out into a csv file
$fn = TempFile csv;
$rl | Export-Csv $fn -NoTypeInformation;
```

See also [TempFile](tempfile.md), [Out-Text](out-text.md).
