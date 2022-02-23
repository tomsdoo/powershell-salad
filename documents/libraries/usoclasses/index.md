# USOClasses
USOClasses(User-Scripted-Object classes) are the classes made of script files that the users script.
The files of USOClasses are written in PowerShell, and placed in [SALADROOT.definition.code](../../design/structures/saladroot.definition.code.md).

The files are named like ```[ClassName].[MethodName].code```.
Two or more files that have same ClassName in the names will be in one class definition, that class will have the methods as the contents of the files.
Like this:
- Human.Cry.code
- Human.Laugh.code

Then, Human class definition is defined. The users can new Human in SALAD.

For example, that would be used like...
``` powershell
(new Human).Cry();
(new Human).Laugh();
```

USOClasses have their methods with try-catch as default.
See [Get-ErrorLog](../functions/get-errorlog.md) and [ErrorLogCollector] for checking logs.

#### Classes
|name|description|
|:--|:--|
| [AccessClient](accessclient.md) | for executing queries to MDB. |
| [ADClient](adclient.md) | for searching from AD. |
| [ADSIClient](adsiclient.md) | for operating with ADSI. |
| [EnDecode](endecode.md) | for operating about encryption. |
| [ErrorLogCollector](errorlogcollector.md) | collects the error log of USOClasses. |
| [EventLogHandler](eventloghandler.md) | fetches the Windows Eventlogs. |
| [ExcelClient](excelclient.md) | for operating with excel, using COM. |
| [IdentifierBase](identifierbase.md) | base class of [SID](sid.md) and [NTAccount](ntaccount.md). |
| [JSHandler](jshandler.md) | provides the functions about JavaScript. |
| [Librarian](librarian.md) | a librarian of SALAD. |
| [MDBBase](mdbbase.md) | provides the functions about MDB. |
| [NTAccount](ntaccount.md) | represents an account. |
| [ProgressManager](progressmanager.md) | provides the functions about the progress bars. |
| [RegistryHandler](resigtryhandler.md) | provides the functions around Windows registries. |
| [RepositoryBase](repositorybase.md) | provides the abstract functions about the repository. |
| [ScreenSnapshot](screensnapshot.md) | provides the functions about screen snapshots. |
| [ScriptHandler](scripthandler.md) | provides the functions around the scripts. It's a base of [VBSHandler]. |
| [SharePointAccessClient](sharepointaccessclient.md) | provides the functions around Sharepoint list. |
| [SharePointGetListCollection](sharepointgetlistcollection.md) | gets the Sharepoint lists information. |
| [SharePointGetUserCollectionFromSite](sharepointgetusercollectionfromsite.md) | gets the users of the Sharepoint site. |
| [SharePointGetViewCollection] | gets the views of the Sharepoint list. |
| [SharePointHandlerBase] | a base of [SharePointGetListCollection], [SharePointGetUserCollectionFromSite], [SharePointGetViewCollection]. |
| [SID](sid.md) | represents SID. |
| [SQLClient] | provides the functions as SQLServer client. |
| [TraceLogCollector] | collects the trace log of SALAD. |
| [VBSHandler](vbshandler.md) | provides the functions around VBScript. |
| [XMLBase]  |provides the functions about the repository in xml. |
