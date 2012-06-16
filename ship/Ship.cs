using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {
  public int actionQueueWidth;
  public int heatDissipation;
  public bool runQueue;
  public Dictionary<string, ModuleInterface> modules = new Dictionary<string, ModuleInterface>();
  public Queue<ModuleAction> actionQueue = new Queue<ModuleAction>();
}
