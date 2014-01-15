using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleAction : MonoBehaviour, ModuleActionInterface {

	public ModuleControllerInterface moduleController;

	public float heatGeneration;

	// public delegate void ModuleActionHandler(ModuleActionInterface sender);
	// public event ModuleActionHandler DoModuleAction;
	// public event ModuleActionHandler EditModuleAction;

	public event EventHandler<ModuleActionArgs> DoModuleAction;
	public event EventHandler<ModuleActionArgs> EditModuleAction;

	protected virtual void OnDoModuleAction () {

		// Make a copy to be more thread-safe
		EventHandler handler = DoModuleAction;

		if (handler != null) {
			handler(this,  EventArgs.Empty);
		}
	}

	protected virtual void OnEditModuleAction () {

		// Make a copy to be more thread-safe
		EventHandler handler = EditModuleAction;

		if (handler != null) {
			handler(this,  EventArgs.Empty);
		}
	}

}