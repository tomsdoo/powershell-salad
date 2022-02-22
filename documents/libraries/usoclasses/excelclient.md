# ExcelClient
This class provides the functions with Excel.  
COM components about Excel are used inside.

#### Methods
- MakeCSVFromCLIXML($CLIXMLFileName, $CSVFileName)
- MakeXLS($filename, $sheetname)
- MakeXLSFromCLIXML($CLIXMLFileName, $XLSFileName)
- MergeXLSFromXLS($FromFileName, $ToFileName, $ToSheetName)
- SaveXLSFromCSV($CSVFileName, $XLSFileName)

*** 
#### MakeCSVFromCLIXML($CLIXMLFileName, $CSVFileName)
This method converts the xml file to csv file.

##### Parameters
|name|description|
|:--|:--|
| $CLIXMLFileName | XML file name to read. |
| $CSVFileName | CSV file name to write. |

##### Returns
nothing.

``` powershell
$ec = new ExcelClient;
$ec.MakeCSVFromCLIXML($xmlfilename, $csvfilename);
```
***

#### MakeXLS($filename, $sheetname)
This method makes a .xls file.

##### Parameters
|name|description|
|:--|:--|
| $filename | xls file name to write. |
| $sheetname | a name of the sheet to create. |

##### Returns
nothing.

``` powershell
$ec = new ExcelClient;
$ec.MakeXML("c:\temp\test.xls", "testsheet");
```

***

#### MakeXLSFromCLIXML($CLIXMLFileName, $XLSFileName)
This method generates a .xls file from a .xml file.

##### Parameters
|name|description|
|:--|:--|
| $CLIXMLFileName | XML file name to read. |
| $XLSFileName | xls file name to write. |

##### Returns
nothing.

``` powershell
$ec = new ExcelClient;
$xmlfn = "c:\temp\service.xml";
Get-WmiObject -ComputerName $env:computername -Namespace root\cimv2 -Class Win32_service | Export-CliXml $xmlfn;
$ec.MakeXLSFromCLIXML($xnlfn, ($xmlfn + ".xls"));
```
***

#### MergeXLSFromXLS($FromFileName, $ToFileName, $ToSheetName)
This method merges the .xls files.

##### Parameters
|name|description|
|:--|:--|
| $FromFileName | a file name to merge from. |
| $ToFileName | a file name to merge into. |
| $ToSheetName | sheet name in $ToFileName. |

##### Returns
nothing.

``` powershell
$ec = new ExcelClient;
$fromxls = "c:\temp\from.xls";
$toxls = "c:\temp\to.xls";
$ec.makexls($fromxls, "fromsheet");
$ec.makexls($toxls, "tosheet");
$ec.MergeXLSFromXLS($fromxls, $toxls, "tosheet");
```

***

#### SaveXLSFromCSV($CSVFileName, $XLSFileName)
This method reads a .csv file and saves a .xls file.

##### Parameters
|name|description|
|:--|:--|
| $CSVFileName | csv file name to read. |
| $XLSFileName | xls file name to write. |

##### Returns
nothing.

``` powershell
$csvfn = "c:\temp\test.csv";
$ec = new ExcelClient;
$ec.SaveXLSFromCSV($csvfn, ($csvfn + ".xls"));
```
