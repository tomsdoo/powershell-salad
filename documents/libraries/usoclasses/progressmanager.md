# ProgressManager
This class provides the functions about the progress bars.  
The users are to refer this as $ProgressManager in the session.


#### NewHandle()
This method gets a new number of the progress bar.


#### Reflect($title, $status, $index, $id)
This method reflects the progress bar.

##### Parameters
|name|description|
|:--|:--|
| $title | a title of the progress bar. |
| $status | a status message of the progress bar. |
| $index | an index of the progress, from 0 to 100. |
| $id | an id of the progress bar. |

##### Returns
nothing.

#### Examples
``` powershell
$myid = $ProgressManager.NewHandle();
1..100 | %{$ProgressManager.Reflect($_.ToString(), $_.ToString(), $_, $myid)};
```
