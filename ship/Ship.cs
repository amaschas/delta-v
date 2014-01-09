using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
  // public int actionQueueWidth;
  public int heatDissipation;
  // public bool runQueue;
  public Dictionary<string, ModuleControllerInterface> modules = new Dictionary<string, ModuleControllerInterface>();
  public Queue<ModuleAction> actionQueue = new Queue<ModuleAction>();
}