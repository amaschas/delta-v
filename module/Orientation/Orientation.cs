using UnityEngine;
using System;
using System.Collections;

public class Orientation : Module {

  public float deltaYaw = 0.0f;
  public float deltaPitch = 0.0f;
  public Vector3 target;
  public bool isRunning;

    // New Messaging stuff
    // *******************
    public delegate void ModuleStateChangeHandler(object sender, ModuleStateChangeArgs e);
    public event ModuleStateChangeHandler StateChange;

    // protected virtual void OnStateChange(EventArgs e) {
    // Lets not get too fancy here
    void OnStateChange(ModuleStateChangeArgs e) {
      if(StateChange != null)
        StateChange(this, e);
    }
    // *******************

  void Start () {
    // OnStateChange(new ModuleStateChangeArgs(transform.position, transform.forward));
  }

	void FixedUpdate () {
    if (deltaYaw != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.up, deltaYaw);
      target = GetTrueForward(20);
      deltaYaw = 0.0f;

      OnStateChange(new ModuleStateChangeArgs(transform.position, transform.forward));
    }
    if (deltaPitch != 0.0f) {
      transform.RotateAround(transform.position, transform.parent.right, deltaPitch);
      target = GetTrueForward(20);
      deltaPitch = 0.0f;

      OnStateChange(new ModuleStateChangeArgs(transform.position, transform.forward));
    }
	}

  public Vector3 GetTrueForward (int zoomFactor) {
    return transform.position + transform.forward * zoomFactor;
  }
}
