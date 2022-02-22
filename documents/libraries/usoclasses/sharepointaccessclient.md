# SharePointAccessClient
This class provides the functions around Sharepoint lists.
JET or ACE required.

### Properties
| Name | Summary |
|:--|:--|
| Engine | JET or ACE. |
| ListGUID | a string of Sharepoint list GUID. |
| URL | a string of Sharepoint site URL. |
| ViewGUID | a string of Sharepoint list view GUID. |

### Methods
- ChangeEngine($enginestr)
- ExecuteSelectQuery($sql)
- GenerateConnectionString($write)
- SetListGUID($listguid)
- SetURL($url)
- SetViewGUID($viewguid)

#### Examples
``` powershell
$myurl = "http://myserver/";
$mylistname = "mylist";
$viewname = "myview";

# get list guid
$listretriever = (new SharePointGetListCollection).Initialize($myurl).SetCredential();
$listguid = $listretriever.GetListGUIDFromListName($mylistname);

# get view guid
$viewretriever = (new SharePointGetViewCollection).Initialize($myurl, $mylistname).SetCredential();
$viewguid = $viewretriever.GetViewGUIDFromViewName($viewname);

# create an instance and set engine
$sacc = (new SharePointAccessClient).ChangeEngine("ACE");
# set url, list GUID and view GUID
$sacc = $sacc.SetURL($myurl).SetListGUID($listguid).SetViewGUID($viewguid);
# execute query
$ret = $sacc.ExecuteSelectQuery("select * from list");
# check the result out
$ret;
```

See also [SharePointGetListCollection], [SharePointGetViewCollection].

#### ChangeEngine($enginestr)
This method sets the engine.

##### Parameters
| name | description |
|:--|:--|
| $enginestr | a string which is "JET" or "ACE" |

##### Returns
this instance.


#### ExecuteSelectQuery($sql)
This method returns the result of the query.

##### Parameters
| name | description |
|:--|:--|
| $sql | a string what is a SQL. |

##### Returns
an array of records.

#### GenerateConnectionString($write)
This methods returns the connection string.

##### Parameters
| name | description |
|:--|:--|
| $write | if to write, it should be true. and else it should be false or null. |

##### Returns
a connection string.


#### SetListGUID($listguid)
This method sets the list GUID in the property.

##### Parameters
| name | description |
|:--|:--|
| $listguid | a string of list GUID. |

##### Returns
this instance.


#### SetURL($url)
This method sets the URL into the property.

##### Parameters
| name | description |
|:--|:--|
| $url | a string of URL. |

##### Returns
this instance.

#### SetViewGUID($viewguid)
This method sets the view GUID into the property.

##### Parameters
| name | description |
|:--|:--|
| $viewguid | a string of view GUID. |

##### Returns
this instance.
