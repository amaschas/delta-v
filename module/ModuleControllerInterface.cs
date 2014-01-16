using UnityEngine;
using System.Collections;

public interface ModuleControllerInterface {

	string Name ();

  void SelectModule ();
  void DeselectModule ()

  // Events
	event EventHandler<ModuleActionArgs> NewModuleAction;
	event EventHandler<ModuleActionArgs> ModuleActionStarted;
	event EventHandler<ModuleActionArgs> ModuleActionFinished;
}