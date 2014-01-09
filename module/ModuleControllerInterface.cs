using UnityEngine;
using System.Collections;

// Should be ModuleControllerInterface
public interface ModuleControllerInterface {

	string Name();

  ModuleControllerInterface ActivateView ();
  void DeactivateView ();

  // This event is fired when a new action is added from the module interface
  // It should send the ModuleActionInterface to the listener (ship)
	public delegate void ModuleHandler(ModuleControllerInterface sender, ModuleActionArgs args);
	public event ModuleHandler NewModuleAction;

	// Sent when an new action is received and started
	public event ModuleHandler ModuleActionStarted;

	// Sent when a module finished an action
	public event ModuleHandler ModuleActionFinished;
}

class ModuleActionArgs : EventArgs {
	public ModuleActionInterface action;
}