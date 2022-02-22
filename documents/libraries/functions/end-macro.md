# End-Macro
``` powershell
End-Macro()
```
This function ends recording macro.  
History information is used in recording macro.  
If the user input [Start-Macro](start-macro.md) then [Start-Macro](start-macro.md) will execute Clear-History inside.
And ```End-Macro``` will generate a function file into [SALADROOT.scripts.macrounits](../../design/structures/saladroot.scripts.macrounits.md).

See also [Recording Macro](../../design/misc/about-macro.md).
