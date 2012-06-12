using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientationController : MonoBehaviour, ModuleInterface {

  public Orientation orientation;
  private OrientationView orientationView;
  // This should maybe be in the model, but it's a giant fuck to put it there
  public Dictionary <string, ModuleInterface> shipModules;
  private float orientationYaw = 0.0f;
  private float orientationPitch = 0.0f;
  private Quaternion lookRotation;

	void Start () {
    shipModules = transform.parent.gameObject.GetComponent<ShipController>().modules;
    orientation = gameObject.GetComponent<Orientation>();
    orientationView = gameObject.GetComponent<OrientationView>();
    orientation.durationModifier = transform.parent.gameObject.GetComponent<Ship>().rotationRate;
    orientationView.enabled = false;
    orientation.isRunning = false;
	}

  public string Name () {
    return transform.name;
  }

  public ModuleInterface ActivateView () {
    orientationView.enabled = true;
    return this;
  }

  public void DeactivateView () {
    orientationView.enabled = false;
  }

  public bool HasAction () {
    if(GetDuration() > 0) {
      return true;
    }
    else return false;
  }

  public ModuleAction GetAction () {
    ModuleAction action = new ModuleAction(this, orientation.target, GetDuration());
    orientation.target = Vector3.zero;
    return action;
  }

  public void SetOrientation (float yaw, float pitch) {
    if(yaw != orientationYaw) {
      orientation.deltaYaw = yaw - orientationYaw;
      orientationYaw = yaw;
    }
    if(pitch != orientationPitch) {
      orientation.deltaPitch = pitch - orientationPitch;
      orientationPitch = pitch;
    }
  }

  public void Run (ModuleAction action) {
    if(!orientation.isRunning) {
      orientation.isRunning = true;
      lookRotation = Quaternion.LookRotation(action.target - transform.parent.position, transform.parent.up);
    }
    if(Quaternion.Angle(transform.parent.rotation, lookRotation) > .1) {
      transform.parent.gameObject.GetComponent<ShipController>().Reorient(lookRotation);
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
    }
    else {
      duration = Quaternion.Angle(transform.parent.rotation, transform.rotation);
    }
    return duration;
  }
}
