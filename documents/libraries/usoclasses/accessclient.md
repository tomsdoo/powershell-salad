# AccessClient
This class provides the functions to access MDB files.  
COM components, JET, and ACE are used inside this class.  
So the users should pay attention for bit-mode.

#### Methods
- CreateMDB($filename)
- ExecuteNonQuery($mdbfilename, $sql)
- ExecuteSelectQuery($mdbfilename, $sql)

##### CreateMDB($filename)
This method creates MDB file.
MSAccess COM components are used inside.

###### Parameters
|name|description|
|:--|:--|
| $filename | a file name to create. |

###### Returns
nothing.

###### example
``` powershell
$acc = new AccessClient;
$acc.CreateMDB("c:\temp\test.mdb");
```

##### ExecuteNonQuery($mdbfilename, $sql)
This method executes insert or update SQL with MDB file.

###### Parameters
|name|description|
|:--|:--|
| $mdbfilename | a file name. |
| $sql | SQL to execute. |

###### Returns
a count of affected rows.

###### example
``` powershell
$mdb = "c:\temp\test.mdb";
$sql = "insert into [TableName] ([ColumnName1], [ColumnName2]) values ('Value1', 'Value2')";
$acc = new accessclient;
$acc.ExecuteNonQuery($mdb, $sql);
```

##### ExecuteSelectQuery($mdbfilename, $sql)
This method executes select SQL with MDB file.

###### Parameters
|name|description|
|:--|:--|
| $mdbfilename | a file name. |
| $sql | SQL to execute. |

###### Returns
an array of the records.

###### example
``` powershell
$mdb = "c:\temp\test.mdb";
$sql = "select * from [TableName]";
$acc = new accessclient;
$rl = $acc.ExecuteSelectQuery($mdb, $sql);
$rl;
```
