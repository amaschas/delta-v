using UnityEngine;
using System.Collections;

// TODO: implement ModuleController
public interface ModuleControllerInterface {

	string Name ();

	float HeatRate ();

  ModuleControllerInterface ActivateView ();
  void DeactivateView ();

  // This event is fired when a new action is added from the module interface
  // It should send the ModuleActionInterface to the listener (ship)
	// delegate void ModuleHandler(ModuleControllerInterface sender, ModuleActionArgs args);
	// event ModuleHandler NewModuleAction;
	public event EventHandler<ModuleActionArgs> NewModuleAction;

	// Sent when an new action is received and started
	// event ModuleHandler ModuleActionStarted;
	public event EventHandler<ModuleActionArgs> ModuleActionStarted;

	// Sent when a module finished an action
	// event ModuleHandler ModuleActionFinished;
	public event EventHandler<ModuleActionArgs> ModuleActionFinished;
}

class ModuleActionArgs : EventArgs {
	public ModuleActionInterface action;
}