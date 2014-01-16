using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleController : MonoBehaviour, ModuleControllerInterface {

	protected EventManager eventManager;

	void Start () {
		module = gameObject.GetComponent<ModuleInterface>();
		moduleView = gameObject.GetComponent<ModuleViewInterface>();

		moduleView.AddModuleAction += CreateNewModuleAction();
	}

	public string Name () {
		return transform.name;
	}

	public void SelectModule ( ModuleActionArgs args = ModuleActionArgs.Empty ) {
		OnModuleSelected(args);
	}

	public void DeselectModule () {
		OnModuleDeselected();
	}

	public void CreateNewModuleAction() {
		// Get the current module state from the module

		// Create new module action args

		// The ship listens to the associated event and uses the sender arg to link the module
		OnNewModuleAction(args);
	}

	public event EventHandler<ModuleActionArgs> ModuleSelected;
	public event EventHandler<ModuleActionArgs> ModuleDeselected;
	public event EventHandler<ModuleActionArgs> NewModuleAction;
	// public event EventHandler<ModuleActionArgs> DoModuleAction;
	public event EventHandler<ModuleActionArgs> ModuleActionStarted;
	public event EventHandler<ModuleActionArgs> ModuleActionFinished;

	protected virtual void OnModuleSelected (ModuleActionArgs args) { eventManager.RaiseEvent(ModuleSelected, args); }
	protected virtual void OnModuleDeselected () { eventManager.RaiseEvent(ModuleDeselected); }
	protected virtual void OnNewModuleAction (ModuleActionArgs args) { eventManager.RaiseEvent(NewModuleAction, args); }
	// protected virtual void OnDoModuleAction (ModuleActionArgs args) { eventManager.RaiseEvent(DoModuleAction, args); }
	protected virtual void OnModuleActionStarted () { eventManager.RaiseEvent(ModuleActionStarted); }
	protected virtual void OnModuleActionFinished () { eventManager.RaiseEvent(ModuleActionFinished); }

}