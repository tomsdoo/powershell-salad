# ScreenSnapshot
This class provides the functions about screen snapshots.

### Methods
- Initialize($folder)
- Record($times, $interval)
- SlideShow()

#### Examples
``` powershell
$ss = new ScreenSnapshot;
$ss = $ss.Initialize("c:\temp");
$ss.Record(60, 1000);
$ss.SlideShow();
```

#### Initialize($folder)
This method initializes the class.
This method sets the directory for saving the image files.


#### Record($times, $interval)
This method starts recording the screen snapshots.

##### Parameters
| name | description |
|:-- |:--|
| $times | count of recording times. |
| $interval | milliseconds of the interval. |

##### Returns
nothing.


#### SlideShow()
This method opens the image files on IE, like slide show.

##### Parameters
nothing.

##### Returns
nothing.
