# SharePointGetUserCollectionFromSite
This class provides the functions of GetUserCollectionFromSite method of Sharepoint web services.

### Base
[SharePointHandlerBase]

### Properties
| Name | description |
|:--|:--|
| SiteUrl | target site url. |

### Methods
- GenerateCommand($xml)
- Initialize($siteurl)
- Interpret($xmlstr)

### Examples
``` powershell
# define URL
$url = "http://myserver/";
# create retriever
$retriever = new SharePointGetUserCollectionFromSite;
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
$ret = (new SharePointGetUserCollectionFromSite).Initialize($url).SetCredential().Get();
# check the list
$ret;
```

#### GenerateCommand($xml)
This method generates a System.Xml.XmlElement instance for GetUserCollectionFromSite command.
This method would be called by GenerateXml method.(SharePointHandlerBase.GenerateXml)

##### Parameters
| Name | description |
|:--|:--|
| $xml | an instance of System.Xml.XmlDocument. |

##### Returns
an instance of System.Xml.XmlElement.

#### Initialize($siteurl)
This method initializes the properties of this instance.

##### Parameters
| Name | description |
|:--|:--|
| $siteurl | Sharepoint site url, like "http://whatever.domain/" |

##### Returns
this instance.

#### Interpret($xmlstr)
This method interprets XML string.

##### Parameters
| Name | description |
|:--|:--|
| $xmlstr | XML string |

##### Returns
an array of XML elements.
