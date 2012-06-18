using UnityEngine;
using System.Collections;

public class Orientation : Module {

  public float deltaYaw = 0.0f;
  public float deltaPitch = 0.0f;
  public Vector3 target;
  public bool isRunning;
	
  // This needs to move to the controller
	void FixedUpdate () {
    if (deltaYaw != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.up, deltaYaw);
      target = GetTrueForward(20);
      deltaYaw = 0.0f;
    }
    if (deltaPitch != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.right, deltaPitch);
      target = GetTrueForward(20);
      deltaPitch = 0.0f;
    }
	}

  public Vector3 GetTrueForward (int zoomFactor) {
    return transform.position + transform.forward * zoomFactor;
  }
}
