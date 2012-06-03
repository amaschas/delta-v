var actionQueueWidth : int;
var heatDissipation : int;
var rotationSpeed : float;
var lineMaterial : Material;

private var modules : GameObject[];
private var actionQueue : GameObject[];

private var lookRotation : Quaternion;
private var velocityVector : VectorLine;
private var MoveShip : System.Boolean;
private var velocityLinePoints : Vector3[];
private var target : Vector3;
private var targetSet : System.Boolean = false;


function Start () {	
	//VectorLine.Destroy (UILine);
	var lineWidth = 5;
	var lineType = LineType.Discrete;
	var joins = Joins.None;
	velocityLinePoints = new Vector3[500];
	velocityLinePoints[0] = Vector3.one;
	velocityVector = VectorLine("Velocity", velocityLinePoints, Color.green, lineMaterial, lineWidth, lineType, joins);
}

function FixedUpdate () {
	if(MoveShip) {
		if(!targetSet) {
      target = transform.Find("Orientation").GetComponent(OrientationDebug).GetTrueForward();
      lookRotation = Quaternion.LookRotation(target - transform.position, transform.position.up);
      targetSet = true;
    }
	  if(Quaternion.Angle(transform.rotation, lookRotation) > .1) {
	    transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
	  }
	  else {
	    MoveShip = false;
      // transform.Find("Orientation").GetComponent(OrientationDebug).ResetPosition();
		  rigidbody.AddForce(transform.forward * 1000, ForceMode.Impulse);
      targetSet = false;
	  }
	}
}

function LateUpdate () {
  velocityLinePoints[0] = transform.position;
  velocityLinePoints[1] = transform.position + rigidbody.velocity * 10;
  velocityVector.Draw3DAuto();
}

public function PlayShipMoves () {
	Debug.Log('Moves Played');
	MoveShip = true;
}

// function RotateShip 