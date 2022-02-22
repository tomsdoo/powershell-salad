# Start-Macro
``` powershell
Start-Macro()
```

This function starts recording macro.  
History information is used in recording macro.  
If the user input Start-Macro then this function will execute Clear-History inside.
And [End-Macro](end-macro.md) will generate a function file into [SALADROOT.scripts.macrounits](../../design/structures/saladroot.scripts.macrounits.md).

See also [Recording Macro](,,.,,.design/misc/about-macro.md).
