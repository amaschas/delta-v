using UnityEngine;
using System.Collections;

public interface ModuleActionInterface {

	// Module to which the action belongs
	ModuleControllerInterface module;

	// Heat generation of this module (will be up to the module whether this is over time or per activation)
	float heatGeneration;

	// Module event delegate
	// delegate void ModuleActionHandler(ModuleActionInterface sender);

	// Triggered (likely by the ship) to the subscribed module when the action is to be performed
	// event ModuleActionHandler DoModuleAction;

	// Triggered (by the ship again) when this action is to be edited
	// event ModuleActionHandler EditModuleAction;

	event EventHandler<ModuleActionArgs> DoModuleAction;

	event EventHandler<ModuleActionArgs> EditModuleAction;

} 