using UnityEngine;
using System.Collections;

public interface ModuleActionInterface {

	// Maybe a Dict for the actual module action?

	// Heat generation of this module (will be up to the module whether this is over time or per activation)
	float heatGeneration;

	// Events
	event EventHandler<ModuleActionArgs> DoModuleAction;
	event EventHandler<ModuleActionArgs> EditModuleAction;

} 