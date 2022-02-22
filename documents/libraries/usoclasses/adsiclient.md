# ADSIClient
This class provides the functions what use ADSI.

#### Methods
- AddLocalUser($localuserid, $password)
- AddLocalUserToGroup($groupname, $localuserid)
- DisableLocalUser($localuserid)
- EnableLocalUser($localuserid)
- GetLocalGroupNames()
- GetLocalUserNames()
- GetMemberOf($groupname)
- Initialize($computername)
- InterpretUserFlags($localuserid)
- RemoveLocalUser($localuserid)
- RemoveLocalUserFromGroup($groupname, $localuserid)
- SetLocalUserPassword($localuserid, $password)

#### Examples
``` powershell
#new ADSIClient
$adsic = new ADSIClient;
#initialize
$adsic = $adsic.Initialize($env:computername);
#see local groups
$adsic.GetLocalGroupNames();

#see local users
$adsic.GetLocalUserNames();

#make a local user
$username = "testuser";
$adsic.AddLocalUser($username, "P@ssw0rd");
$adsic.SetLocalUserPassword($username, "P@ssw0rd!");

#check result
$adsic.GetLocalUserNames();

#see user flags
$adsic.InterpretUserFlags($username);

#disable a local user
$adsic.DisableLocalUser($username);

#check disabled user
$adsic.InterpretUserFlags($username);

#enable a local user
$adsic.EnableLocalUser($username);

#check enabled user
$adsic.InterpretUserFlags($username);

#list member of Administrators
$adsic.GetMemberOf("Administrators");

#add a local user to administrators
$adsic.AddLocalUserToGroup("Administrators", $username);

#check member of Administrators
$adsic.GetMemberOf("Administrators");

#remove a local user to administrators
$adsic.RemoveLocalUserFromGroup("Administrators", $username);

#check member of Administrators
$adsic.GetMemberOf("Administrators");

#remove a local user
$adsic.RemoveLocalUser($username);

#check result
$adsic.GetLocalUserNames();
```

#### AddLocalUser($localuserid, $password)
This method adds a local user to the target computer.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a string which is a local user name. |
| $password | a string which is a password of the user. |

##### Returns
nothing.


#### AddLocalUserToGroup($groupname, $localuserid)
This method adds a local user to a local group.

##### Parameters
|name|description|
|:--|:--|
| $groupname | a local group name. |
| $localuserid | a local user name. |

##### Returns
nothing.


#### DisableLocalUser($localuserid)
This method disables a local user.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a local user name. |

##### Returns
nothing.


#### EnableLocalUser($localuserid)
This method enables a local user.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a local user name. |

##### Returns
nothing.


#### GetLocalGroupNames()
This method lists local groups of the target computer.

##### Returns
an array of the local group names.


#### GetLocalUserNames()
This method lists local users of the target computer.

##### Returns
an array of the local user names.


#### GetMemberOf($groupname)
This method lists the members of the local group of the target computer.

##### Parameters
|name|description|
|:--|:--|
| $groupname | a local group name. |

##### Returns
an array of the members of the group.


#### Initialize($computername)
This method initializes the target computer of this class.

##### Parameters
|name|description|
|:--|:--|
| $computername | target computer name. |

##### Returns
this instance.

#### InterpretUserFlags($localuserid)
This method interprets the user flags of the local user on the targetcomputer.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a local user name. |

##### Returns
a set of user flags.

#### RemoveLocalUser($localuserid)
This method removes a local user from the target computer.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a local user name. |

##### Returns
nothing.


#### RemoveLocalUserFromGroup($groupname, $localuserid)
This method removes a local user from the local group on the target computer.

##### Parameters
|name|description|
|:--|:--|
| $groupname | a local group name. |
| $localuserid | a local user name. |

##### Returns
nothing.


#### SetLocalUserPassword($localuserid, $password)
This method sets a password for the local user on the target computer.

##### Parameters
|name|description|
|:--|:--|
| $localuserid | a local user name. |
| $password | a password for the user. |

##### Returns
nothing.
