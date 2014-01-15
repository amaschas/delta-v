using UnityEngine;
using System.Collections;

// TODO: implement ModuleController
public interface ModuleControllerInterface {

	EventManager eventManager;

	string Name ();

	// float HeatRate ();

  void SelectModule ();
  void DeselectModule ()

  // This event is fired when a new action is added from the module interface
  // It should send the ModuleActionInterface to the listener (ship)
	// delegate void ModuleHandler(ModuleControllerInterface sender, ModuleActionArgs args);
	// event ModuleHandler NewModuleAction;
	event EventHandler<ModuleActionArgs> NewModuleAction;

	// Sent when an new action is received and started
	// event ModuleHandler ModuleActionStarted;
	event EventHandler<ModuleActionArgs> ModuleActionStarted;

	// Sent when a module finished an action
	// event ModuleHandler ModuleActionFinished;
	event EventHandler<ModuleActionArgs> ModuleActionFinished;
}