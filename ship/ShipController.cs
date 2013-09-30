using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour, ShipInterface {

	private Ship ship;
	private ShipView shipView;
	public Dictionary<string, ModuleInterface> modules;
	public Queue<ModuleAction> actionQueue;

	//This should not exist
	private OrientationController orientationController;

	// This should be in Ship.cs
	private ModuleInterface activeModule = null;

    // void Start() { Init(); }
    // void OnEnable() { Init(); }

	public void Start () {
		Debug.Log("Initializing " + transform.name);
		ship = gameObject.GetComponent<Ship>();
		shipView = gameObject.GetComponent<ShipView>();
		modules = ship.modules;
		actionQueue = ship.actionQueue;
		// This should probably not exist;
		// orientationController = transform.Find("Orientation").GetComponent<OrientationController>();
		// shipView.enabled = false;
		// ship.runQueue = false;
		foreach (Transform child in transform) if (child.CompareTag("Module")) {
			Debug.Log("Adding " + child.name);
			// Can't I just add the names to the interface?
			ModuleInterface controller = child.gameObject.GetComponent(typeof(ModuleInterface)) as ModuleInterface;
      ship.modules.Add(child.name, controller);
    }

	}

	public string Name () {
		return transform.name;
	}

	void FixedUpdate () {
		// This should listen to an GameController event
		// I wonder if this even needs to be in fixedupdate, given that the actions themselves play in an update
		// Literally the shipcontroller could send the start event, the individual ships should start the first action in the queue,
		// When the actions finishes, it messages the shipcontroller, and the shipcontroller starts the next event
		// The only update stuff happens in the modules
		// if(ship.runQueue && GameObject.Find("GameController").GetComponent<GameController>().turnPlaying) {
		// 	RunQueue();
		// }
	}

	public void Select () {
		shipView.enabled = true;
		// orientationController.ActivateView();
	}

	public void Deselect () {
		shipView.enabled = false;
		// orientationController.DeactivateView();
		// if(activeModule != null) {
		// 	activeModule.DeactivateView();
		// }
	}

	public void ActivateModule (ModuleInterface module) {
		if(activeModule != null) {
			module.DeactivateView();
		}
		activeModule = module.ActivateView();
	}

	public void AddCurrentActionToQueue () {
		// Debug.Log(orientationController.HasAction());
		// The modules should talk to orientatiocontroller if they need to
		// if(orientationController.HasAction()) {
		// 	ship.actionQueue.Enqueue(orientationController.GetAction());
		// }
		// perhaps activeModule.GetActions() should return an array?
		if(activeModule != null && activeModule.HasAction()) {
			ship.actionQueue.Enqueue(activeModule.GetAction());
		}
	}

	// public void RunQueue () {
	// 	if(ship.actionQueue.Count > 0) {
	// 		ship.runQueue = true;
	// 		ModuleAction action = ship.actionQueue.Peek();
	// 		action.DoAction();
	// 		// Need to do this with an event listener
	// 		// rather than use the ship interface
	// 		if(!ship.modules[action.module.name].IsRunning()) {
	// 			ship.actionQueue.Dequeue();
	// 		}
	// 	}
	// 	else {
	// 		ship.runQueue = false;
	// 	}
	// }

	// Need a way for module sto hook onto ship behaviors, maybe use ship interface?
	public void Reorient (Quaternion rotation, float rate) {
		// Debug.Log(Quaternion.Angle(transform.rotation, lookRotation));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rate);
	}

	public void AddThrust (int thrust) {
		rigidbody.AddForce(transform.forward * thrust, ForceMode.Impulse);
	}

	public Vector3 GetVelocity () {
		return transform.position + rigidbody.velocity * 10;
	}

	public Vector3 GetTransformPosition () {
		return transform.position;
	}
}
