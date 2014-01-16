using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleAction : MonoBehaviour, ModuleActionInterface {

	protected EventManager eventManager;

	public ModuleControllerInterface moduleController;

	public float heatGeneration;

	public event EventHandler<ModuleActionArgs> DoModuleAction;
	public event EventHandler<ModuleActionArgs> EditModuleAction;

	protected virtual void OnDoModuleAction () { eventManager.RaiseEvent(DoModuleAction); }
	protected virtual void OnEditModuleAction () { eventManager.RaiseEvent(EditModuleAction); }

}