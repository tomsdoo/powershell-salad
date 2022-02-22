function global:ShowGUI-Curious()
#VISIBILITY:public
{
	(New-Object SALAD.Curious.MDIFormClass).ShowDialog() | Out-Null;
}
