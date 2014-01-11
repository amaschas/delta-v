using UnityEngine;
using System.Collections;
using System.Collections.Generic;

// Might need an interface for this
public class Ship : MonoBehaviour {
	// Need a way to track ship damage state
  // public int actionQueueWidth;
  public int heatDissipation;
  // public bool runQueue;
  public Dictionary<string, ModuleControllerInterface> modules = new Dictionary<string, ModuleControllerInterface>();
  public Queue<ModuleAction> actionQueue = new Queue<ModuleAction>();
}