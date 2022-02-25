# SharePointHandlerBase
This class provides the functions around Sharepoint Web services.
This class shoud be not used without inheritting.

### Properties
| name | description |
|:--|:--|
| Credential | credential for Sharepoint Web services. |
| MethodURL | means what's name. |
| SOAPAction | means what's name. |

### Methods
- ClearCredential()
- GenerateXml()
- Get()
- SendAndReceive()
- SetCredential($cred)

For example, see [SharePointGetListCollection](sharepointgetlistcollection.md), [SharePointGetUserCollectionFromSite](sharepointgetusercollection.md), [SharePointGetViewCollection](sharepointgetviewcollection.md).

#### ClearCredential()
This method clears the Credential property of the instance.

##### Parameters
nothing.

##### Returns
this instance.


#### GenerateXml()
This method generates System.Xml.XmlDocument and returns it.

##### Parameters
nothing.

##### Returns
an instance of System.Xml.XmlDocument.

#### Get()
This method returns data of the query.

##### Parameters
nothing.

##### Returns
something data of the query.

#### SendAndReceive()
This method sends and receives HTTP message, and returns it.

##### Parameters
nothing.

##### Returns
XML string.


#### SetCredential($cred)
This method sets Credential property.
If $cred parameter is null, the dialog of Inquiry-for-credential will be shown.

##### Parameters
| name | description |
|:--|:--|
| $cred | an instance of System.Management.Automation.PSCredential. |

##### Returns
this instance.
