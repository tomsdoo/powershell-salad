function global:ShowGUI-CommunicationClient()
#VISIBILITY:public
{
	(New-Object SALAD.TCP.ChatForm).ShowDialog() | Out-Null;
}
