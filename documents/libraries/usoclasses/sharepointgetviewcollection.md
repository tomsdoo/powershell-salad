# SharePointGetViewCollection
This class provides the functions of GetViewCollection method of Sharepoint web services.

### Base
[SharePointHandlerBase]

### Properties
| name | description |
|:--|:--|
| SiteURL | target site url |
| ListName | target list name |

### Methods
- GenerateCommand($xml)
- GetViewGUIDFromViewName($viewname)
- Initialize($siteurl, $listname)
- Interpret($xmlstr)

### Examples
``` powershell
# define URL
$url = "http://myserver/";
$listname = "mylist";
# create retriever
$retriever = new SharePointGetViewCollection;
# initialize retriever
$retriever = $retriever.Initialize($url, $listname);
# get the lists
$ret = $retriever.Get();
# check the lists
$ret;
```

``` powershell
# using other credentials
$url = "http://myserver/";
$listname = "mylist";
# use SetCredential method
$ret = (new SharePointGetViewCollection).Initialize($url, $listname).SetCredential().Get();
# check the list
$ret;
```

#### GenerateCommand($xml)
This method generates a System.Xml.XmlElement instance for GetViewCollection command.
This method would be called by GenerateXml method.(SharePointHandlerBase.GenerateXml)

##### Parameters
| name | description |
|:--|:--|
| $xml | an instance of System.Xml.XmlDocument. |

##### Returns
an instance of System.Xml.XmlElement.


#### GetViewGUIDFromViewName($viewname)
This method returns the view GUID.

##### Parameters
| name | description |
 |:--|:--|
| $viewname | means what's name. |

##### Returns
a string of GUID.

#### Initialize($siteurl, $listname)
This method initializes the properties of this instance.

##### Parameters
|name | description |
|:--|:--|
| $siteurl | Sharepoint site url, like "http://whatever.domain/" |
| $listname | list name, like "mylist" |

##### Returns
this instance.


#### Interpret($xmlstr)
This method interprets XML string.

##### Parameters
|name | description |
|:--|:--|
| $xmlstr | XML string |

##### Returns
an array of XML elements.
