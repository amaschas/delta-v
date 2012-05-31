private var pitch : float = 0.0;
private var yaw : float = 0.0;

function OnGUI () {
	var rect = Rect(20, 20, 150, 170);
	canClick = (rect.Contains(Event.current.mousePosition)? false : true);
	GUILayout.BeginArea(rect);
	GUI.contentColor = Color.black;
	GUI.contentColor = Color.white;
	if (GUILayout.Button("Play", GUILayout.Width(150))) {
		PlayMoves();
	}
	if (GUILayout.Button("Pause", GUILayout.Width(150))) {
		Pause();
	}
	yaw = GUILayout.HorizontalSlider(yaw, -10.0, 10.0);
	pitch = GUILayout.VerticalSlider(pitch, -10.0, 10.0);
	GUILayout.EndArea();
}

function PlayMoves () {
  var ship = GameObject.Find('galactica');
  var ShipController = ship.GetComponent(ShipController);
  ShipController.PlayShipMoves();
}

function Pause () {
  var ship = GameObject.Find('galactica');
  ship.rigidbody.constraints = RigidbodyConstraints.FreezeNone;
  ship.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
}