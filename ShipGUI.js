var lineMaterial : Material;

private var pitch : float = 0.0;
private var yaw : float = 0.0;
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
	yaw = GUILayout.HorizontalSlider(yaw, -10.0, 10.0);
	pitch = GUILayout.VerticalSlider(pitch, -10.0, 10.0);
	GUILayout.EndArea();
}

function FixedUpdate () {
	// Debug.Log(selected.transform.position);
	// Debug.Log(selected.transform.forward);
	if(yaw == 0 && pitch == 0) {
		linePoints[0] = selected.transform.position;
		linePoints[1] = selected.transform.forward * 20;
		HeadingIndicator.Draw3DAuto();
	}
	else {
		Debug.Log('triggered');
		var temp = selected.transform;
		temp.rotation = Quaternion.AngleAxis(30, selected.transform.up);
		HeadingIndicator.Draw3DAuto(temp);
	}
}

function PlayMoves () {
  var ShipController = selected.GetComponent(ShipController);
  ShipController.PlayShipMoves();
}

function Pause () {
  selected.rigidbody.constraints = RigidbodyConstraints.FreezeNone;
  selected.rigidbody.constraints = RigidbodyConstraints.FreezeAll;
}