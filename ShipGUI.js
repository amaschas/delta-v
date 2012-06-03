var lineMaterial : Material;

private var pitch : float = 0.0;
private var yaw : float = 0.0;
private var prevPitch : float = 0.0;
private var prevYaw : float = 0.0;
private var selected : GameObject;

private var HeadingIndicator : VectorLine;
private var linePoints : Vector3[];

selected = GameObject.Find('galactica');

function Start () {
	var lineWidth = 5;
	var lineType = LineType.Discrete;
	var joins = Joins.None;
	linePoints = new Vector3[500];
	linePoints[0] = Vector3.one;
	HeadingIndicator = VectorLine("UI", linePoints, Color.white, lineMaterial, lineWidth, lineType, joins);
}

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
	yaw = GUILayout.HorizontalSlider(yaw, 0.0, 360.0);
	pitch = GUILayout.VerticalSlider(pitch, 0.0, 360.0);
	GUILayout.EndArea();
}

function FixedUpdate () {
	var orientation = selected.transform.Find("Orientation");
		
	if(yaw != prevYaw) {
		orientation.transform.RotateAround(orientation.transform.position, selected.transform.right, yaw - prevYaw);
		prevYaw = yaw;
	}
	if(pitch != prevPitch) {
		orientation.transform.RotateAround(orientation.transform.position, selected.transform.up, pitch - prevPitch);
		prevPitch = pitch;
	}
  // linePoints[0] = orientation.transform.position;
  // linePoints[1] = orientation.transform.forward * 20;
  // HeadingIndicator.Draw3DAuto();
}

function PlayMoves () {
	pitch = 0.0;
	yaw = 0.0;
  var ShipController = selected.GetComponent(ShipController);
  ShipController.PlayShipMoves();
}

function Pause () {
  selected.rigidbody.constraints = RigidbodyConstraints.FreezeNone;
  selected.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
}