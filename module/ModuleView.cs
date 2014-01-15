using UnityEngine;
using System.Collections;

public class ModuleView : MonoBehaviour, ModuleActionInterface {

	protected EventManager eventManager;

	void Start () {
		// Get module controller
		moduleController = gameObject.GetComponent<ModuleControllerInterface>();

		// Set up event listeners
		moduleController.ModuleSelected += ActivateModuleView;
		moduleController.ModuleDeselected += DeactivateModuleView;
	}

	void ActivateModuleView () {
		// activate self
		// this.enabled = true;
	}

	void DeactivateModuleView () {
		// deactivate self
		// this.enabled = false;
	}

	// public delegate void ModuleViewHandler(ModuleViewInterface sender, ModuleActionArgs args);
	// public event ModuleViewHandler AddModuleAction;
	public event EventHandler<ModuleActionArgs> AddModuleAction;

	protected virtual void OnAddModuleAction () { eventManager.RaiseEvent(AddModuleAction, GetModuleAction()); }

	public ModuleActionArgs GetModuleAction() {
		return new ModuleActionArgs();
	}

	// Renders any global module UI elements
	public void RenderGlobalModuleUI () {
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 50, 100, 20), "Add Action")) {
      OnAddModuleAction();
    }
	}

}