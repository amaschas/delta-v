using UnityEngine;
using System.Collections;

public class ModuleView : MonoBehaviour, ModuleActionInterface {

	// public delegate void ModuleViewHandler(ModuleViewInterface sender, ModuleActionArgs args);
	// public event ModuleViewHandler AddModuleAction;
	public event EventHandler<ModuleActionArgs> AddModuleAction;

	public void OnAddModuleAction () {

		// Make a copy to be more thread-safe
		ModuleViewHandler handler = AddModuleAction;

		if (handler != null) {
			action = GetModuleAction();
			handler(this, action);
		}
	}

	public ModuleActionArgs GetModuleAction() {
		return new ModuleAction();
	}

	// Renders any global module UI elements
	public void RenderGlobalModuleUI () {
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 50, 100, 20), "Add Action")) {
      OnAddModuleAction();
    }
	}

}