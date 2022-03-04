# XMLBase
This class provides the functions about the repository in xml.
This class inherits [RepositoryBase](repositorybase.md).

### Methods
- Load()
- Reflect()
- Restructure($obj)

### Examples
``` powershell
# new instance
$xb = new XMLBase;
# initialize
$xb = $xb.InitializeRepository("c:\temp\test.xml");
# load data
$xb.Load();
# create new entry
$e = $xb.NewEntry();
# reflection
$x.Restructure($e);
$x.Reflect();
```

#### Load()
This method loads the data.


#### Reflect()
This method writes data into the xml file.

#### Restructure($obj)
This method restructures the record sets on memory of the class.
The users calls this method before Reflect().
