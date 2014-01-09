using UnityEngine;
using System.Collections;

public interface ModuleActionInterface {
	public ModuleControllerInterface module;
	public float heatGeneration;
	public delegate void ModuleActionHandler(ModuleActionInterface sender);
	public event ModuleActionHandler DoModuleAction;
} 