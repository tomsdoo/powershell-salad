# SharePointGetListCollection
This class provides the functions of GetListCollection method of Sharepoint web services.

### Base
[SharePointHandlerBase]

### Properties
| Name | Summary |
|:--|:--|
| SiteUrl | target site url. |

### Methods
- GenerateCommand($xml)
- GetListGUIDFromListName($listname)
- Initialize($siteurl)
- Interpret($xmlstr)

#### Examples
``` powershell
# define URL
$url = "http://myserver/";
# create retriever
$retriever = new SharePointGetListCollection;
# initialize retriever
$retriever = $retriever.Initialize($url);
# get the lists
$ret = $retriever.Get();
# check the lists
$ret;
```

``` powershell
# using other credentials
$url = "http://myserver/";
# use SetCredential method
$ret = (new SharePointGetListCollection).Initialize($url).SetCredential().Get();
# check the list
$ret;
```

#### GenerateCommand($xml)
This method generates a System.Xml.XmlElement instance for GetListCollection command.
This method would be called by GenerateXml method.([SharePointHandlerBase.GenerateXml])

##### Parameters
| Name | Summary |
|:--|:--|
| $xml | an instance of System.Xml.XmlDocument. |

##### Returns
an instance of System.Xml.XmlElement.

#### GetListGUIDFromListName($listname)
This method returns the list GUID.

##### Parameters
| Name | Summary |
|:--|:--|
| $listname | means what's name. |

##### Returns
a string of GUID.

##### Initialize($siteurl)
This method initializes the properties of this instance.

##### Parameters
| Name | Summary |
|:--|:--|
| $siteurl | Sharepoint site url, like "http://whatever.domain/" |

##### Returns
this instance.

#### Interpret($xmlstr)
This method interprets XML string.

##### Parameters
| Name | Summary |
|:--|:--|
| $xmlstr | XML string |

##### Returns
an array of XML elements.
