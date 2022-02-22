# About MDB

#### Requirements
MSAccess and JET on the computer

#### Select from table
``` powershell
$mdb = "c:\temp\test.mdb";
$sql = "select * from [TestTable]";
$acc = new AccessClient;
$acc.ExecuteSelectQuery($mdb, $sql);
```

#### Insert into table
``` powershell
$mdb = "c:\temp\test.mdb";
$sql = "insert into [TestTable] ([ColA],[ColB]) values ('ValueA', 'ValueB')";
$acc = new AccessClient;
$acc.ExecuteNonQuery($mdb, $sql);
```
