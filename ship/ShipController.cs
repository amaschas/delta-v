using UnityEngine;
using System.Collections;


public class ShipController : MonoBehaviour {

	private Ship ship;
	private ShipView shipView;
	private OrientationController orientationController;
	private GameObject activeModule;

    void Start() { Init(); }
    void OnEnable() { Init(); }

	public void Init () {
		ship = gameObject.GetComponent<Ship>();
		shipView = gameObject.GetComponent<ShipView>();
		orientationController = transform.Find("Orientation").GetComponent<OrientationController>();
		shipView.enabled = false;
		ship.runQueue = false;
		//Initialize modules with array of module GameObjects;
	}

	// Update is called once per frame
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

	public void ActivateModule (GameObject module) {
		if(activeModule) {
			activeModule.SendMessage("DeactivateView");
		}
		activeModule = module;
		module.SendMessage("ActivateView");
	}

	public void AddCurrentActionToQueue () {
		Debug.Log(orientationController.HasAction());
		if(orientationController.HasAction()) {
			ship.Enqueue(orientationController.GetAction());
		}
		ModuleActions activeModuleActions = activeModule.GetComponent(typeof(ModuleActions)) as ModuleActions;
		if(activeModuleActions.HasAction()) {
			ship.Enqueue(activeModuleActions.GetAction());
		}
	}

	public void RunQueue () {
		if(ship.actionQueue.Count > 0) {
			ship.runQueue = true;
			ModuleAction action = ship.actionQueue.Peek();
			action.module.SendMessage("DoAction", action);
			ModuleActions module = action.module.GetComponent(typeof(ModuleActions)) as ModuleActions;
			if(!module.IsRunning()) {
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
