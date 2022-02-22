# Out-Text
``` powershell
Out-Text($any)
```
This function outputs data into a temporary file and displays the file.

``` powershell
Get-WmiObject -ComputerName $env:Computername -Namespace root\cimv2 -Class Win32_Service | Out-Text;
```
