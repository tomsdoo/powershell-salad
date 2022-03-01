# SQLClient
This class provides the functions as SQLServer client.

### Methods
- ExecuteNonQuery($sql)
- ExecuteSelectQuery($sql)
- GetSchema]()
- Initialize($servername)
- SelectDatabase($databasename)

### Examples
``` powershell
$server = "mysqlserver";
# new instance
$sc = new SQLClient;
# initialize
$sc = $sc.Initialize($server);
# get schema information
$sc.GetSchema();

# execute select query
$sql = "select * from [TestTable]";
$sc.ExecuteSelectQuery($sql);

# execute insert SQL
$sql = "insert into [TestTable] ([TestCol]) values ('TestValue')";
$sc.ExecuteNonQuery($sql);
```

#### ExecuteNonQuery($sql)
This method executes the SQL that is not select statement.

##### Parameters
| name | description |
|:--|:--|
| $sql | SQL to execute. |

##### Returns
a count of affected rows.


#### SQLClient.ExecuteSelectQuery($sql)
This method executes the SQL that is select statement.

##### Paramaters
| name | description |
|:--|:--|
| $sql | SQL to execute. |

##### Returns
an array of the records.

#### GetSchema()
This method gets the schema information.

##### Parameters
nothing.

##### Returns
schema data.

#### Initialize($servername)
This method initializes the class.
$servername: SQLServer name.

##### Parameters
| name | description |
|:--|:--|
| $servername | SQL Server name. |

##### Returns
this instance.

#### SelectDatabase($databasename)
This method selects the database(Initial catalog).

##### Parameters
| name | description |
|:--|:--|
| $databasename | database name. |

##### Returns
nothing.
