using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleController : MonoBehaviour, ModuleControllerInterface {

	protected EventManager eventManager;

	void Start () {
		moduleView = gameObject.GetComponent<ModuleViewInterface>();
		module = gameObject.GetComponent<ModuleInterface>();
		// OnModuleSelected();
	}

	public string Name () {
		return transform.name;
	}

	/// Note sure if this should take action args or just an action
	// Has to pass this arg onto the event somehow?
	// Can obviate this by have moduels track their own queue
	public void SelectModule ( ModuleActionArgs args = ModuleActionArgs.Empty ) {
		OnModuleSelected();
	}

	public void DeselectModule () {
		OnModuleDeselected();
	}

	public event EventHandler<ModuleActionArgs> ModuleSelected;
	public event EventHandler<ModuleActionArgs> ModuleDeselected;
	public event EventHandler<ModuleActionArgs> NewModuleAction;
	public event EventHandler<ModuleActionArgs> ModuleActionStarted;
	public event EventHandler<ModuleActionArgs> ModuleActionFinished;

	protected virtual void OnModuleSelected () { eventManager.RaiseEvent(ModuleSelected); }
	protected virtual void OnModuleDeselected () { eventManager.RaiseEvent(ModuleDeselected); }
	protected virtual void OnNewModuleAction () { eventManager.RaiseEvent(NewModuleAction); }
	protected virtual void OnModuleActionStarted () { eventManager.RaiseEvent(ModuleActionStarted); }
	protected virtual void OnModuleActionFinished () { eventManager.RaiseEvent(ModuleActionFinished); }

	// public ModuleControllerInterface ActivateView () {
	// 	engineView.enabled = true;
	// 	return this;
	// }

	// public void DeactivateView () {
	// 	engineView.enabled = false;
	// }

}