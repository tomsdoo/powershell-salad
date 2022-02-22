# new
This is an important function for the extended class library named User-Scripted-Object.

For example, the files named ```Librarian.*.code``` placed in [SALADROOT.definition.code](saladroot.definition.code.md) are imported in the session as the USO class named ```Librarian```.
```Librarian``` class is designed as a librarian of the environment.

The users who want to know the names of the available USOClasses ask to Librarian.

``` powershell
$librarian = new Librarian;
$librarian.GetAvailableClassNames();
```
