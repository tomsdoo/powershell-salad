# Search In Active Directory

#### Required
The computer that runs SALAD is joining whatever ActiveDirectory.

#### Search user-self
``` powershell
$adc = new ADClient;
$adc.Search($env:username);
```
