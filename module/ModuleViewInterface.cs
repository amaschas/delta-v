using UnityEngine;
using System.Collections;

public interface ModuleViewInterface {

	EventManager eventManager;

	ModuleControllerInterface moduleController;

	void DctivateView ();

	void DeactivateView ();

	// delegate void ModuleViewHandler(ModuleViewInterface sender, ModuleActionArgs args);
	// event ModuleViewHandler AddModuleAction;
	event EventHandler<ModuleActionArgs> AddModuleAction;

	void OnAddModuleAction ();

	ModuleActionArgs GetModuleAction ();

	void RenderGlobalModuleUI ();

} 