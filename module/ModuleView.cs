using UnityEngine;
using System.Collections;

public class ModuleView : MonoBehaviour, ModuleActionInterface {

	protected EventManager eventManager;

	void Start () {
		// Get module
		module = gameObject.GetComponent<ModuleInterface>();

		// Get module controller
		moduleController = gameObject.GetComponent<ModuleControllerInterface>();

		// Set up event listeners
		moduleController.ModuleSelected += ActivateModuleView;
		moduleController.ModuleDeselected += DeactivateModuleView;
		DeactivateModuleView();
	}

	void ActivateModuleView () {
		// activate self
		// this.enabled = true;
	}

	void DeactivateModuleView () {
		// deactivate self
		// this.enabled = false;
	}

	public event EventHandler<ModuleActionArgs> AddModuleAction;

	// This doesn't send the action because the module controller constructs the action
	// The module view should send the UI input to the controller, which will store the settings in the module
	// This only notifies the controller that it should create a module action from the current module state
	protected virtual void OnAddModuleAction () { eventManager.RaiseEvent(AddModuleAction); }

	// Renders any global module UI elements
	public void RenderGlobalModuleUI () {
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 50, 100, 20), "Add Action")) {
      OnAddModuleAction();
    }
	}

}