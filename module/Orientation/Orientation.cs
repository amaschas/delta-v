using UnityEngine;
// Might not need System here anymore without EventArgs
using System;
using System.Collections;

public class Orientation : Module {

  public bool isRunning;

  public void ChangeOrientation (float yaw, float pitch) {
    transform.RotateAround(transform.position, transform.parent.up, yaw);
    transform.RotateAround(transform.position, transform.parent.right, pitch);
    OnStateChange();
  }

  // public Vector3 GetTrueForward (int zoomFactor) {
  //   return transform.position + transform.forward * zoomFactor;
  // }
}
