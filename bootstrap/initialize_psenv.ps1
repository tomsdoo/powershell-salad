param([string]$rootfolder)
	## Load Temporary Prompt
	import-module -name ($rootfolder + "bootstrap" + ([system.io.path]::DirectorySeparatorChar.ToString()) + "dummyprompt.ps1");

	$host.UI.RawUI.ForegroundColor = [system.consolecolor]::Green;
	$host.UI.RawUI.BackgroundColor = [system.consolecolor]::Black;
	Clear-host;
	$host.UI.RawUI.CursorSize = 10;
	$host.UI.RawUI.BufferSize.Height = 300;
	$host.UI.RawUI.BufferSize.Width = 150;

	$LOADING_CLASSES_MESSAGE = "Now loading classes...";
	$SETTING_PSMODULES_MESSAGE = "Now setting PowerShell modules...";
	$LOADING_USO_MESSAGE = "Now loading user-scripted-object classes...";
	$READY_MESSAGE = "Ready...";
	$DIR_SEP_CHAR = [System.IO.Path]::DirectorySeparatorChar.ToString();
	
	## Ready Message
	Write-Host ((Get-Date -format "yyyy/MM/dd HH:mm:ss") + " " + $READY_MESSAGE);

	## Setting Message
	Write-Host ((Get-Date -format "yyyy/MM/dd HH:mm:ss") + " " + $SETTING_PSMODULES_MESSAGE);

	## Initialize PowerShell Scripting Environment
	$psscriptfiles = Get-ChildItem ($rootfolder + "scripts" + $DIR_SEP_CHAR) -recurse | ?{$_.attributes -notlike "*directory*"};
	foreach($psscriptfile in $psscriptfiles)
	{
		if($psscriptfile.name -ne "prompt.ps1")
		{
			Import-Module -name $psscriptfile.fullname;
		}
	}

	## Set Bit Mode
	$global:BitMode = [IntPtr]::Size * 8;

	$global:SessionManager = object-sessionmanager -filename ($rootfolder + "session.registry");
	$global:SessionManager = $SessionManager.Add("system.session.directory.root", $rootfolder);
	$global:SessionManager = $SessionManager.Add("system.session.environment.version", "SALAD G.1 V.1.0.0");
	$global:SessionManager = $SessionManager.Add("system.session.environment.account", $env:username);
	$global:SessionManager = $SessionManager.Add("system.session.environment.computer", $env:computername);
 	$global:SessionManager = $SessionManager.Add("system.session.directory.bootstrap", ("@_system.session.directory.root_@bootstrap" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.classes", ("@_system.session.directory.root_@classes" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.log", ("@_system.session.directory.root_@log" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.definition", ("@_system.session.directory.root_@definition" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.definition.system", ("@_system.session.directory.definition_@system" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.definition.code", ("@_system.session.directory.definition_@code" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.scripts", ("@_system.session.directory.root_@scripts" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.directory.macrounits", ("@_system.session.directory.scripts_@macrounits" + $DIR_SEP_CHAR));
	$global:SessionManager = $SessionManager.Add("system.session.definition.html.style", "<style type= `"text/css`">table{border:1px #999 solid;}td{border:1px #999 solid; border-collapse:collapse;word-break:keep-all;}</style>");

	## C# Utility Setup
	. (($SessionManager.GetValue("system.session.directory.bootstrap"))+ "initialize_sharputil.ps1");

	## Define UserSettings
	. (($SessionManager.GetValue("system.session.directory.bootstrap"))+ "initialize_userenv.ps1");

	## Define AssemblyNames
	. (($SessionManager.GetValue("system.session.directory.bootstrap"))+ "initialize_assemblyenv.ps1");

	## Load User-Scripted Object Class Code
	Write-Host ((Get-Date -format "yyyy/MM/dd HH:mm:ss") + " " + $LOADING_USO_MESSAGE);
	$global:CodeManager = object-codemanager;
	$bgfl = Get-ChildItem ($SessionManager.GetValue("system.session.directory.definition.code")) -recurse | ?{$_.attributes -notlike "*directory*"} | ?{$_.extension -eq ".code"} | %{New-Object bgfilereader($_.FullName)};
	while($true)
	{
		[System.Threading.Thread]::Sleep(10);
		if(($bgfl | ?{$_.busy}) -eq $null)
		{
			break;
		}
	}
	foreach($bgfe in $bgfl)
	{
		$global:CodeManager = $CodeManager.Add([System.IO.Path]::GetFileNameWithoutExtension($bgfe.filename), $bgfe.resultlines);
	}
	$host.UI.RawUI.WindowTitle = $SessionManager.GetValue("system.session.environment.account") + " at " + $SessionManager.GetValue("system.session.environment.version") + " on " + $SessionManager.GetValue("system.session.environment.computer");

	## Setting Massage
	Write-Host ((Get-Date -format "yyyy/MM/dd HH:mm:ss") + " " + $LOADING_CLASSES_MESSAGE);
	Load-Classes;

	## Progress Manager
	$global:ProgressManager = new progressmanager;

	$logomessage = "SCRIPTING ENVIRONMENT FOR","ALMIGHTY","ADMINISTRATION";
	$nickname = "SALAD";
	#splash -logo $logomessage -nickname $nickname;

	## Start Transcript
	$logfilenamebase = $SessionManager.GetValue("system.session.directory.log") + (Get-Date -format "yyyyMMdd") + "_" + $SessionManager.GetValue("system.session.environment.computer") + "_" + $SessionManager.GetValue("system.session.environment.account");
	$transcriptlogfilename = $logfilenamebase + "_transcript_" + (Get-Date -format "yyyyMMddHHmmssfff") + ".log";
	$dummy = Start-Transcript -path $transcriptlogfilename;

	## Clear Host
	Clear-Host;

	$adminlist = Get-ChildItem ($SessionManager.GetValue("system.session.directory.definition.system") + "administrator" + $DIR_SEP_CHAR) | %{Get-Content $_.fullname};
	$global:badm = $false;
	foreach($admine in $adminlist)
	{
		$global:badm = $badm -or ($SessionManager.GetValue("system.session.environment.account") -eq $admine);
	}

	if($badm -eq $false)
	{
		$publist = Get-ChildItem ($SessionManager.GetValue("system.session.directory.definition.system") + "publiccommand" + $DIR_SEP_CHAR) | %{Get-Content $_.fullname};
		$comml = Get-Command;
		foreach($pube in $publist)
		{
			$comml = $comml | ?{$_.name -ne $pube};
		}
		$comml | %{$_.visibility = "private"};
	}

	## Setting Location
	Set-Location ($SessionManager.GetValue("system.session.directory.root"));

	## SALAD C# Classes Setting
	. (($SessionManager.GetValue("system.session.directory.bootstrap"))+ "initialize_saladassemblyenv.ps1");	

	## Orthodox Prompt Will Be Loaded When Environment Is Completely Initialized
	Get-ChildItem ($SessionManager.GetValue("system.session.directory.scripts")) -recurse | ?{$_.name -eq "prompt.ps1"} | %{import-module -name $_.fullname};

	## TabExpansion
	$global:GLib = new Librarian;
	. (($SessionManager.GetValue("system.session.directory.bootstrap")) + "TabExpansion.ps1");
