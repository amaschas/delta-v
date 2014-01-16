using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleAction : MonoBehaviour, ModuleActionInterface {

	protected EventManager eventManager;

	public ModuleControllerInterface moduleController;

	public float heatGeneration;

	Start () {
		moduleController = gameObject.GetComponent<ModuleControllerInterface>();
	}

	// Events
	public event EventHandler<ModuleActionArgs> DoModuleAction;
	public event EventHandler<ModuleActionArgs> EditModuleAction;

	// Event tirggers
	protected virtual void OnDoModuleAction () { eventManager.RaiseEvent(DoModuleAction); }
	protected virtual void OnEditModuleAction () { eventManager.RaiseEvent(EditModuleAction); }

}