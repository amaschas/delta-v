using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Might need an interface for this
public class Ship : MonoBehaviour {

	// TODO: ship damage state

	public float heatMax;

  public float heatDissipationRate;

  public Dictionary<string, ModuleControllerInterface> modules = new Dictionary<string, ModuleControllerInterface>();

  public OrderedDictionary<ModuleControllerInterface, ModuleActionArgs> moduleActions = new OrderedDictionary<ModuleControllerInterface, ModuleActionArgs>();

  public List<ModuleControllerInterface> activeModules = new List<ModuleControllerInterface>();
}