using UnityEngine;
using System.Collections;

public interface ModuleViewInterface {

	delegate void ModuleViewHandler(ModuleViewInterface sender, ModuleActionArgs args);
	event ModuleViewHandler AddModuleAction;

	void OnAddModuleAction ();

	ModuleActionArgs GetModuleAction ();

	void RenderGlobalModuleUI ();

} 