var actionQueueWidth : int;
var heatDissipation : int;
var rotationSpeed : float;
var lineMaterial : Material;

private var ShipIntersectHit;
private var lookRotation;
private var UILine : VectorLine;
private var velocityVector : VectorLine;
private var MoveShip;
private var linePoints : Vector3[];
private var velocityLinePoints : Vector3[];
private var modules : GameObject[];
private var actionQueue : GameObject[];

function Start () {
	//VectorLine.Destroy (UILine);
	var lineWidth = 5;
	var lineType = LineType.Discrete;
	var joins = Joins.None;
	linePoints = new Vector3[500];
	linePoints[0] = Vector3.one;
	//velocityLinePoints = linePoints;
	velocityLinePoints = new Vector3[500];
	velocityLinePoints[0] = Vector3.one;
	UILine = VectorLine("UI", linePoints, Color.white, lineMaterial, lineWidth, lineType, joins);
	velocityVector = VectorLine("Velocity", velocityLinePoints, Color.green, lineMaterial, lineWidth, lineType, joins);
}

function FixedUpdate ()
{
  //Debug.Log(rigidbody.velocity);
  //Debug.Log(MoveShip);
  // if(MoveShip == true) {
  //  linePoints[0] = transform.position;
  //  linePoints[1] = ShipIntersectHit;
  // }
	if(Input.GetMouseButtonDown(1)) {
	  MoveShip = false;
		var ShipIntersect = new Plane(Camera.main.transform.forward, transform.position);
		var dist : float = 0.0;
		var hit : RaycastHit;
		var DestIntersect : Ray =  Camera.main.ScreenPointToRay(Input.mousePosition);   
		ShipIntersect.Raycast(DestIntersect, dist);
		ShipIntersectHit = DestIntersect.GetPoint(dist);
		lookRotation = Quaternion.LookRotation(ShipIntersectHit - transform.position, transform.position.up);
		//VectorLine.DestroyLine(UILine);
		// linePoints[0] = transform.position;
		// linePoints[1] = ShipIntersectHit;
		// UILine.Draw3DAuto();
	}
	if(MoveShip) {
	  if(Quaternion.Angle(transform.rotation, lookRotation) > .1) {
	    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	  }
	  else {
	    MoveShip = false;
	    // rigidbody.AddForce(ShipIntersectHit * 10, ForceMode.Impulse);
		rigidbody.AddForce(transform.forward * 1000, ForceMode.Impulse);
	  }
	}
}

function LateUpdate () {
  //var locVel = transform.InverseTransformDirection(transform.forward);
  // var locVel = transform.InverseTransformDirection(rigidbody.velocity);
  // locVel.z = 20;
  // var v = transform.TransformDirection(locVel);
  // Debug.Log(v);
  //Debug.Log(transform.InverseTransformDirection(rigidbody.velocity));
  velocityLinePoints[0] = transform.position;
  //velocityLinePoints[1] = transform.forward * 10;
  velocityLinePoints[1] = transform.position + rigidbody.velocity * 10;
  velocityVector.Draw3DAuto();
}

public function PlayShipMoves () {
	Debug.Log('Moves Played');
	MoveShip = true;
	//RotateToMovement(ShipIntersectHit);
}

function RotateToMovement(target) {
	var lookRotation = Quaternion.LookRotation(target - transform.position, transform.position.up);
	while(transform.rotation != lookRotation) {
		//transform.localEulerAngles.x = 0;
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, 10*Time.deltaTime);
	}
	rigidbody.AddForce(ShipIntersectHit, ForceMode.Impulse);
}