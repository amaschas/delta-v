using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipController : MonoBehaviour, ShipControllerInterface {


	/**
	 * Class members
	 * -------------
	 */

	// Ship data object
	private Ship ship;

	// Ship view object
	private ShipView shipView;

	// Registry of modules via their interface
	public Dictionary<string, ModuleControllerInterface> modules;

	// Queued module actions
	public OrderedDictionary<ModuleControllerInterface, ModuleActionArgs> moduleActions;

	// The module action currently being edited in the GUI
	public ModuleAction selectedModuleAction;

	// The currently active module in the GUI
	private ModuleControllerInterface activeModule = null;


	/**
	 * Monobehavior methods
	 * --------------------
	 */

	/**
	 * Monobehavior start
	 */
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

	/**
	 * Monobehavior update
	 */
	void Update () {
		// Check the action queue
	}


	/**
	 * Protected class members
	 * -----------------------
	 */

	/**
	 * Initialization functionality for modules on ship start
	 */
	protected void InitModule( ModuleControllerInterface moduleController ) {

		// Add the module to the ship's module registry
		ship.modules.Add(moduleController.Name(), moduleController);

		// Connect ship to module events
		moduleController.NewModuleAction += AddNewModuleAction;
	}

	/**
	 * Adds a new module action in the appropriate place in the queue
	 */
	protected void AddNewModuleAction(object sender, ModuleActionArgs args) {

		// Add action to timeline
		// This needs to insert the action based on the time offset of the action
		moduleActions.Add(sender, args);
	}


	/**
	 * Public interface methods
	 * ------------------------
	 */

	/**
	 * Gets the ship name
	 */
	public string Name () {
		return transform.name;
	}

	/**
	 * Enables the ship GUI
	 */
	public void Select () {
		shipView.enabled = true;
		// orientationController.ActivateView();
	}

	/**
	 * Disables the ship GUI
	 */
	public void Deselect () {
		shipView.enabled = false;
	}

	/**
	 * Reorients the ship model
	 */
	public void Reorient (Quaternion rotation, float rate) {
		// Debug.Log(Quaternion.Angle(transform.rotation, lookRotation));
		transform.rotation = Quaternion.RotateTowards(transform.rotation, rotation, Time.deltaTime * rate);
	}

	/**
	 * Adds thrust to the ship rigidbody
	 */
	public void AddThrust (int thrust) {
		rigidbody.AddForce(transform.forward * thrust, ForceMode.Impulse);
	}

	/**
	 * Gets the ship velocity
	 */
	public Vector3 GetVelocity () {
		return transform.position + rigidbody.velocity * 10;
	}

	/**
	 * Gets the ship transform position
	 */
	public Vector3 GetTransformPosition () {
		return transform.position;
	}
}
