# Generate-Life
``` powershell
Generate-Life(
  [parameter(mandatory=$true)][int]$row,
  [parameter(mandatory=$true)][int]$column,
  [parameter(mandatory=$true)][int]$generation,
  $deadchar,
  $alivechar
)
```
This function displays Life game.

#### Parameters
|name|necessary|description|
|:--|:--|:--|
| row | Yes | the number of rows. |
| column | Yes | the number of columns. |
| generation | Yes | the number of generations. |
| deadchar | No | the character(string) for the dead cell. |
| alivechar | No | the character(string) for the alive cell. |

``` powershell
Generate-Life -row 20 -column 30 -generation 10;
#input dead and alive characters
Generate-Life -row 20 -column 30 -generation 10 -deadchar " " -alivechar "/";
```
