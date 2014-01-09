using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ModuleAction : MonoBehaviour, ModuleActionInterface {

	public ModuleControllerInterface module;
	
	public float heatGeneration;

	public delegate void ModuleActionHandler(ModuleActionInterface sender);
	public event ModuleActionHandler DoModuleAction;

}