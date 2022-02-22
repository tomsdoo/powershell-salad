function global:splash()
#VISIBILITY:public
{
	$logo = @();
	$logo += "SCRIPTING ENVIRONMENT FOR";
	$logo += "ALMIGHTY";
	$logo += "ADMINISTRATION";
	splashcore -logo $logo -nickname "SALAD";
}
