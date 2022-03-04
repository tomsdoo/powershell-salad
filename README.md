# PowerShell SALAD
Can be the operating environment for all administrators.
For the users who want...
- text files so that they can customize in their local environments.
- to use the scripts with no installations.
- to read and understand the environments.
- to modify the scripts by their own.
- to put the function files easily to use.
- all scripts in one directory.
- [to reuse VBScript or JavaScript](documents/design/misc/about-legacy-scripts.md).
- [to do something with SharePoint by scripts](documents/design/misc/about-sharepoint.md).
- [to search objects from Active Directory](documents/design/misc/about-adsi.md).
- to make my-script-library.
- [to output the logs automatically in the operations](documents/design/misc/about-logging.md).
- [to customize tab expansion](documents/design/misc/about-tab-expansion.md).
- [to manage the global variables in one interface](documents/design/structures/core.sessionmanager.md).

All of files are text files in order that the admins can read.
It's a package to use and customize by yourselves.


## Get started
You put it, and it works.

- [using Access MDB](documents/design/misc/about-mdb.md)
- [recording macros](documents/design/misc/about-macro.md)
- use functions

The users can make and use the scripts or some in SALAD by their own.

- Library
  - [Functions](documents/libraries/functions/index.md): the PowerShell functions that the users write.
  - [USOClasses](documents/libraries/usoclasses/index.md): the classes made of script files that the users write.
  - the libraries of the environment can be seen in HTML
  USOClasses, Functions, and C# classes that you made in SALAD can be seen in HTML.

  SALAD has a function ```Make-Library``` to export libraries.
  To export libraries, input following lines.
  ``` powershell
  # at SALAD environment
  $mydir = "c:\temp\WhereverYouCanWrite\";
  Make-Library -folder $mydir;
  ```

- Functions
  - See [How To Add Function](documents/libraries/functions/how-to-add-function.md) to add the functions in SALAD.

- Settings
  - See [How To Add Settings](documents/settings/how-to-add-settings.md) to customize the settings in SALAD.

- USOClasses
  - See [How To Add A USOClass](documents/libraries/usoclasses/how-to-add-usoclass.md) to add USO classes in SALAD.
  - To inherit USO classes, see [How To Inherit USOClass](documents/libraries/usoclasses/how-to-inherit-usoclass.md).

- Structures
  - See [directories](documents/design/structures/directories.md), [Things Core](documents/design/structures/core.md).

- Rules
  - See [Local Rules](documents/design/structures/rules.md).

- Making Package
  - See [About Packaging](documents/design/misc/about-packaging.md).
