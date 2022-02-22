# RepositoryBase
This class provides the abstract functions about the repository.

#### Properties
|name|description|
|:--|:--|
|Repository|This property has the name of the repository.|
|KeyColumnList|This property has an array of the names of the columns that are used as the key columns.|

***

#### Exists($obj)
This method checks if the record set has the object.

##### Parameters
|name|description|
|:--|:--|
| $obj | an object. |

##### Returns
boolean.

***

#### FindIndex($obj)
This method returns the index that the object exists in the record sets.
If the object did not exist in the record sets, this method returns -1.

***
#### GetPropertyNames($obj)
This method returns an array of the names of the properties of the object.

***
#### InitializeRepository($repository, $keylist)
This method initializes the repository information.
$repository is the file name("c:\temp\test.mdb", "c:\temp\test.xml", for example).
$keylist is an array of the names of the key columns.

See [MDBBase](mdbbase.md) for example.

***
#### IsEqual($refobj, $diffobj)
This medhot returns if $refobj is equal to $diffobj or not.
The process refers RepositoryBase.KeyColumnList.

***
#### NewEntry($obj)
This method returns the new record.  
If $obj is not null, this method returns the object that has the properties same as $obj.  
If $obj is null, this method returns the object that has the properties same as RecordSet.  
If $obj is null and RecordSet is null, this method throws an exception.

See [MDBBase](mdbbase.md) for example.
