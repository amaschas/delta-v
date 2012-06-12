using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour {

	private Ship ship;
	private ShipView shipView;
	public Dictionary<string, ModuleInterface> modules;
	public Queue<ModuleAction> actionQueue;

	//This should not exist
	private OrientationController orientationController;

	// This should be in Ship.cs
	private ModuleInterface activeModule;

    void Start() { Init(); }
    void OnEnable() { Init(); }

	public void Init () {
		ship = gameObject.GetComponent<Ship>();
		shipView = gameObject.GetComponent<ShipView>();
		modules = ship.modules;
		actionQueue = ship.actionQueue;
		// This should probably not exist;
		orientationController = transform.Find("Orientation").GetComponent<OrientationController>();
		shipView.enabled = false;
		ship.runQueue = false;
		
		foreach (Transform child in transform) if (child.CompareTag("Module")) {
			ModuleInterface controller = child.gameObject.GetComponent(typeof(ModuleInterface)) as ModuleInterface;
      ship.modules.Add(child.name, controller);
    }
	}

	void FixedUpdate () {
		if(ship.runQueue) {
			RunQueue();
		}
	}

	public void Select () {
		shipView.enabled = true;
		orientationController.ActivateView();
	}

	public void Deselect () {
		shipView.enabled = false;
		orientationController.DeactivateView();
	}

	public void ActivateModule (ModuleInterface module) {
		if(activeModule != null) {
			module.DeactivateView();
		}
		activeModule = module.ActivateView();
	}

	public void AddCurrentActionToQueue () {
		// Debug.Log(orientationController.HasAction());
		if(orientationController.HasAction()) {
			ship.actionQueue.Enqueue(orientationController.GetAction());
		}
		if(activeModule.HasAction()) {
			ship.actionQueue.Enqueue(activeModule.GetAction());
		}
	}

	public void RunQueue () {
		if(ship.actionQueue.Count > 0) {
			ship.runQueue = true;
			ModuleAction action = ship.actionQueue.Peek();
			action.DoAction();
			if(!ship.modules[action.module.Name()].IsRunning()) {
				ship.actionQueue.Dequeue();
			}
		}
		else {
			ship.runQueue = false;
		}
	}

	public void Reorient (Quaternion lookRotation) {
		// Debug.Log(Quaternion.Angle(transform.rotation, lookRotation));
		transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * ship.rotationRate);
	}

	public void AddThrust (int thrust) {
		rigidbody.AddForce(transform.forward * thrust, ForceMode.Impulse);
	}
}
