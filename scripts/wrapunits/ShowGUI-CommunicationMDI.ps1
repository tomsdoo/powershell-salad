function global:ShowGUI-CommunicationMDI()
#VISIBILITY:public
{
	(New-Object SALAD.TCP.ChatMDI).ShowDialog() | Out-Null;
}
