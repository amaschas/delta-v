using UnityEngine;
using System.Collections;

public interface ModuleViewInterface {

	void ActivateView ();

	void DeactivateView ();

	event EventHandler<ModuleActionArgs> AddModuleAction;

	ModuleActionArgs GetModuleAction ();

	void RenderGlobalModuleUI ();

} 