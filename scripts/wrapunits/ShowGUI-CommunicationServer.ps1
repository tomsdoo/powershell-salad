function global:ShowGUI-CommunicationServer()
#VISIBILITY:public
{
	(New-Object SALAD.TCP.ChatRoomForm).ShowDialog() | Out-Null;
}
