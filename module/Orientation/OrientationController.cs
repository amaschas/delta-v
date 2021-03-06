using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientationController : MonoBehaviour, ModuleInterface {

  public Orientation orientation;
  private OrientationView orientationView;
  // This should maybe be in the model, but it's a giant fuck to put it there
  public Dictionary <string, ModuleInterface> shipModules;
  private float prevYaw = 0.0f;
  private float prevPitch = 0.0f;

	void Start () {
    shipModules = transform.parent.gameObject.GetComponent<ShipController>().modules;
    orientation = gameObject.GetComponent<Orientation>();
    orientationView = gameObject.GetComponent<OrientationView>();
    orientationView.enabled = false;
    orientation.isRunning = false;
	}

  public string name {
    get { return transform.name; }
  }

  public ModuleInterface ActivateView () {
    orientationView.enabled = true;
    orientationView.headingIndicator.active = true;
    return this;
  }

  public void DeactivateView () {
    orientationView.headingIndicator.active = false;
    orientationView.enabled = false;
  }

  public bool HasAction () {
    if(GetDuration() > 0) {
      return true;
    }
    else return false;
  }

  public ModuleAction GetAction () {
    // Quaternion lookRotation = Quaternion.LookRotation(orientation.target - transform.parent.position, transform.parent.up);
    Quaternion rotation = transform.rotation;
    ModuleAction action = new ModuleAction(this, rotation, GetDuration());
    // orientation.target = Vector3.zero;
    return action;
  }

  public void SetOrientation (float yaw, float pitch) {
    // if(yaw != orientationYaw) {
    //   orientation.deltaYaw = yaw - orientationYaw;
    //   orientationYaw = yaw;
    // }
    // if(pitch != orientationPitch) {
    //   orientation.deltaPitch = pitch - orientationPitch;
    //   orientationPitch = pitch;
    // }
    orientation.ChangeOrientation(yaw, pitch);
  }

  public void Run (ModuleAction action) {
    if(!orientation.isRunning) {
      orientation.isRunning = true;
    }
    if(Quaternion.Angle(transform.parent.rotation, action.rotationTarget) > .1) {
      transform.parent.gameObject.GetComponent<ShipController>().Reorient(action.rotationTarget, orientation.durationModifier);
    }
    else {
      orientation.isRunning = false;
    }
    transform.rotation = transform.parent.rotation;
  }

  public bool IsRunning () {
    return orientation.isRunning;
  }

  public float GetDuration () {
    float duration;
    if(orientation.durationModifier != 0.0f) {
      duration = Quaternion.Angle(transform.parent.rotation, transform.rotation) / orientation.durationModifier;
      Debug.Log(duration);
    }
    else {
      duration = Quaternion.Angle(transform.parent.rotation, transform.rotation);
    }
    return duration;
  }
}
