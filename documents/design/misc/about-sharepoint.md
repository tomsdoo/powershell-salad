# About SharePoint

The users can get the lists, the views, the users of SharePoint.

#### To get the list names
``` powershell
$lists = Get-SharePointLists -SiteUrl http://myserver/;
$lists;
```

#### To get the view names
``` powershell
$views = Get-SharePointListViews -SiteUrl http://myserver/ -ListName mylist;
$views;
```

#### To get the users
``` powershell
$users = Get-SharePointUsers -SiteUrl http://myserver/;
$users;
```

#### To get the list items
ACE or JET required.
``` powershell
$items = Get-SharePointListItems -SiteUrl http://myserver/ -ListName mylist -WithAce;
$items;
```
