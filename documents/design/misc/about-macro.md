# Recording Macro
Recording macro.

#### To start recording
``` powershell
Start-Macro;
```

#### To end recording
``` powershell
End-Macro;
```

ex) fetch Windows services and display a text file -> macro

``` powershell
Start-Macro;
# recording started

$sl = Get-WmiObject -ComputerName $env:COMPUTERNAME -Namespace root\cimv2 -class win32_service;
$sl | Out-Text;

# end recording
End-Macro;

# check the function file out
explorer $SessionManager.GetValue("system.session.directory.macrounits");
```
