using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OrientationController : MonoBehaviour, ModuleControllerInterface {

	public Orientation orientation;
	private OrientationView orientationView;

	void Start () {
		orientation = gameObject.GetComponent<Orientation>();
		orientationView = gameObject.GetComponent<OrientationView>();
		orientationView.SetActive(false);
		// orientation.isRunning = false;
		// Debug.Log(orientation.isRunning);
	}

	void OnEnable() {
		orientationView.SetActive(true);
	}

	void OnDisable() {
		orientationView.SetActive(false);
	}

  // public void OnSliderChange (float value) {
  //   Debug.Log(value);
  // }

  // public ModuleControllerInterface ActivateView () {
  //   orientationView.enabled = true;
  //   orientationView.headingIndicator.active = true;
  //   return this;
  // }

	// public string name {
	// 	get { return transform.name; }
	// }

	// public ModuleControllerInterface ActivateView () {
	// 	// View should activate itself
	// 	// foreach(var property in orientation.GetType().GetProperties()) {
	// 	// 	Debug.Log(property.Name);
	// 	// }
	// 	orientationView.enabled = true;
	// 	Debug.Log(orientationView.headingIndicator);
	// 	orientationView.headingIndicator.active = true;
	// 	return this;
	// }

	// public void DeactivateView () {
	// 	orientationView.headingIndicator.active = false;
	// 	orientationView.enabled = false;
	// }

	// public bool HasAction () {
	// 	if(GetDuration() > 0) {
	// 		return true;
	// 	}
	// 	else return false;
	// }

	public ModuleAction GetAction () {
		Quaternion rotation = transform.rotation;
		ModuleAction action = new ModuleAction(this, rotation, GetDuration());
		return action;
	}

	public void SetOrientation (float yaw, float pitch) {
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
