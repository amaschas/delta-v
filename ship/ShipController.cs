using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour, ShipInterface {

	// Ship data object
	private Ship ship;

	// Ship view object
	private ShipView shipView;

	// Registry of modules via their interface
	public Dictionary<string, ModuleControllerInterface> modules;

	// The queue of module actions to execute
	// TODO: most actions will not be sequential, so we really need a way to trigger events at x time, in parallel, for y duration, and for events to be linked
	public Queue<ModuleAction> actionQueue;

	// The module action currently being edited in the GUI
	// Should this belong to the ship data object?
	// The principle being that the controller only handles data in a transient fashion?
	public ModuleAction selectedModuleAction;

	// The currently active module in the GUI
	// TODO: can this just be an aspect of selectedModuleAction?
	private ModuleControllerInterface activeModule = null;

	//This should not exist
	private OrientationController orientationController;

    // void Start() { Init(); }
    // void OnEnable() { Init(); }

	public void Start () {
		Debug.Log("Initializing " + transform.name);

		// Init ship data object
		ship = gameObject.GetComponent<Ship>();

		// Init view
		shipView = gameObject.GetComponent<ShipView>();

		modules = ship.modules;

		actionQueue = ship.actionQueue;

		// Get all of the modules and pass their interface to the module initializer
		foreach (Transform child in transform) if (child.CompareTag("Module")) {
			Debug.Log("Adding " + child.name);
			ModuleControllerInterface controller = child.gameObject.GetComponent(typeof(ModuleControllerInterface)) as ModuleControllerInterface;
			InitModule(controller)
    }

	}

	private void InitModule( ModuleControllerInterface moduleController ) {

		// Add the module to the ship's module registry
		ship.modules.Add(moduleController.Name(), moduleController);

		// Connect ship to module events
		moduleController.NewModuleAction += AddNewModuleAction;
	}

	public void AddNewModuleAction(object sender, ModuleActionArgs args) {

		// Get action from event args
		action = ModuleActionArgs.action;

		// Add action to timeline

		return;
	}

	public string Name () {
		return transform.name;
	}

	void Update () {

	}

	// Each update, message all relevant modules on that frame to complete one discrete action
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

	public void ActivateModule (ModuleControllerInterface module) {
		if(activeModule != null) {
			module.DeactivateView();
		}
		activeModule = module.ActivateView();
	}

	public void AddCurrentActionToQueue () {
		if(activeModule != null && activeModule.HasAction()) {
			ship.actionQueue.Enqueue(activeModule.GetAction());
		}
	}

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
