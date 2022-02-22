# Import-Package
``` powershell
Import-Package(
  [parameter(mandatory=$true)][string]$filename
)
```
This function imports from the package xml file that the developer makes.  
From next session, the users can use the Functions or USOClasses what is in the package.  
For developpers, see [Packaging](../../design/misc/about-packaging.md).

``` powershell
Import-Package -filename "c:\temp\package.xml";
```
