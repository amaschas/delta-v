using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleAction : MonoBehaviour, ModuleActionInterface {

	public ModuleControllerInterface moduleController;

	public float heatGeneration;

	public delegate void ModuleActionHandler(ModuleActionInterface sender);
	public event ModuleActionHandler DoModuleAction;
	public event ModuleActionHandler EditModuleAction;

	public void OnDoModuleAction () {

		// Make a copy to be more thread-safe
		ModuleActionHandler handler = DoModuleAction;

		if (handler != null) {
			handler(this);
		}
	}

	public void OnEditModuleAction () {

		// Make a copy to be more thread-safe
		ModuleActionHandler handler = EditModuleAction;

		if (handler != null) {
			handler(this);
		}
	}

}