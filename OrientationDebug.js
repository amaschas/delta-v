#pragma strict

var debug : System.Boolean = false;
var lineMaterial : Material;
private var debugLine : VectorLine;
private var linePoints : Vector3[];

function Start () {
	var lineWidth = 5;
	var lineType = LineType.Discrete;
	var joins = Joins.None;
	linePoints = new Vector3[500];
	linePoints[0] = Vector3.one;
	debugLine = VectorLine("debug", linePoints, Color.blue, lineMaterial, lineWidth, lineType, joins);
}

function Update () {
	if(debug) {
		linePoints[0] = transform.position;
		linePoints[1] = GetTrueForward();
		debugLine.Draw3DAuto();
	}
}

public function GetTrueForward() {
	return transform.position + transform.forward * 20;
}

public function ResetPosition() {
	// transform.rotation = transform.parent.rotation;
}