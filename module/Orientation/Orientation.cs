using UnityEngine;
using System.Collections;

public class Orientation : Module {

  public float deltaYaw = 0.0f;
  public float deltaPitch = 0.0f;
  public Vector3 target;
  public bool isRunning;

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void FixedUpdate () {
    if (deltaYaw != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.up, deltaYaw);
      target = GetTrueForward();
      deltaYaw = 0.0f;
    }
    if (deltaPitch != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.right, deltaPitch);
      target = GetTrueForward();
      deltaPitch = 0.0f;
    }
	}

  public Vector3 GetTrueForward () {
    return transform.position + transform.forward * 20;
  }
}
