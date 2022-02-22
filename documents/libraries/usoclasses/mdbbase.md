# MDBBase
This class provides the functions with MSAccess.  
The methods of this class are using [AccessClient](accessclient.md).  
This class inherits [RepositoryBase](repositorybase.md).


#### InsertSQL($obj)
This method generates the SQL from the parameter and returns it.
MDBBase.ReflectSQL() calls this.



#### Load()
This method reads the recordset and returns the records.
This method sets records into the property named RecordSet.

``` powershell
$mdbfn = "c:\temp\test.mdb";
$m = new MDBBase;
$m = $m.InitializeRepository($mdbfn);
$m.Load();
```

The table named same as the class name should be in the MDB file.
So the users will make some classes that inherit MDBBase and use those classes.

***

#### Reflect(obj)
This method reflects the object(s) into the table of the MDB file.  
The SQL result is to be returned.

``` powershell
$mdbfn = "c:\temp\test.mdb";
$m = new MDBBase;
$m = $m.InitializeRepository($mdbfn);
$l = foreach($idx in (0..99))
{
    $e = $m.newentry();
    $e.ColA = "Value" + $idx.ToString();
    $e.ColB = "Value" + $idx.ToString();
    $e;
}
$resunt = $m.Reflect($l);
```

***


#### ReflectSQL($obj)
This method returns the SQL with the object.  
MDBBase.Reflect() calls this method.

***

#### UpdateSQL($obj)
This method generates the SQL from the parameter and returns it.
MDBBase.ReflectSQL() calls this.

***
