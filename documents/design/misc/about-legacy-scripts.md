# About Legacy Scripts
The users can reuse VBScript in SALAD through ```VBSHandler```.(for JavaScript, ```JSHandler```)

#### Requirements
- registered COM object that is MSScriptControl.ScriptControl
- session what's in 32bit mode

#### Case
There are VBScript files.
Put the files in one directory wherever you like.(for example "c:\temp\vbs")
Load and use them.

The users can new them, call their methods so that USOClasses and Functions can collaborate with VBScript or JavaScript.

VBScript example is below. ```Colony.vbs```
``` vbs
Class RandomMaker
	Private m_min
	Private m_max
	Public Function Initialize(provided_min, provided_max)
		m_min = provided_min
		m_max = provided_max
	End Function
	Public Function Execute()
		Randomize
		Execute = Int((m_max - m_min + 1) * Rnd + m_min)
	End Function
End Class

Class Worker
	Private m_seq
	Public Property Get seq()
		seq = m_seq
	End Property
	Private m_alive
	Public Property Get alive()
		alive = m_alive
	End Property
	Private m_age
	Public Property Get age()
		age = m_age
	End Property
	Public Function Initialize(provided_seq)
		m_seq = provided_seq
		m_alive = True
		m_age = 0
	End Function
	Public Function NextDay()
		If m_alive = True Then
			m_age = m_age + 1
			Set rm = New RandomMaker
			Call rm.Initialize(0, 10)
			zerototen = rm.Execute()
			Select Case zerototen
				Case 9
					m_alive = False
			End Select
		Else
		End If
	End Function
End Class

Class Queen
	Private m_alive
	Public Property Get alive()
		alive = m_alive
	End Property
	Private m_age
	Public Property Get age()
		age = m_age
	End Property
	Public Function Initialize()
		m_alive = true
		m_age = 0
	End Function
	Public Function NextDay()
		Set NextDay = Nothing
		If m_alive = True Then
			m_age = m_age + 1

			Set rm = New RandomMaker
			Call rm.Initialize(0, 10)
			zerototen = rm.Execute()
			Select Case zerototen
				Case 9
					Set wk = New Worker
					Call wk.Initialize(0)
					Set NextDay = wk
			End Select

			If m_age > 50000 Then
				m_alive = False
			End If
		Else
		End If
	End Function
End Class

Class Colony
	Private m_queen
	Public Property Get myqueen()
		Set myqueen = m_queen
	End Property
	Public Property Get age()
		age = m_queen.age
	End Property
	Private m_workers()
	Public Property Get workers()
		workers = m_workers
	End Property
	Public Property Get workercount()
		workercount = UBound(m_workers)
	End Property
	Public Function Initialize()
		Set m_queen = New Queen
		Call m_queen.Initialize()
		ReDim m_workers(0)
	End Function
	Public Function NextDay()
		Set newworker = m_queen.NextDay()
		If newworker Is Nothing Then
		Else
			Redim Preserve m_workers(UBound(m_workers) + 1)
			Set m_workers(UBound(m_workers)) = newworker
		End If
		If UBound(m_workers) > 0 Then
			Dim icnt
			For icnt = 1 To UBound(m_workers)
				Call m_workers(icnt).NextDay()
			Next
		End If
	End Function
End Class
```

And collaborate with PowerShell in SALAD
``` powershell
# create script handler
$b = new VBSHandler;

# initialize
$b = $b.Initialize("C:\temp\vbs");

# new colony class
$classname = "colony";
$colony = $b.New($classname);

# and else, whatever you like

# initialize colony(this is a method that Colony class has in vbs)
$colony.Initialize();

# check colony instance
$colony;

# send 100 days for colony(NextDay is a method that Colony class has in vbs)
0..99 | %{$colony.NextDay()};

# check colony instance
$colony;

# check colony workers
$colony.workers | Group-Object alive;
```

##### about Colony.vbs
###### Colony
- has Queen and Workers.
- gets tomorrow by NextDay method.

###### Queen
- lives 50000 days.
- sometimes bear a Worker, and Worker belongs to Colony.

###### Worker
- sometimes dies.

###### Operation
- create a Colony and give it some days.

The operations in VBS are like following.
``` vbs
Set c = New Colony
Call c.Initialize()
Call c.NextDay()
MsgBox (Cstr(c.age))
```
