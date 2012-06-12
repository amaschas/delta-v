using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

  public int actionQueueWidth;
  public int heatDissipation;
  public float rotationRate;
  public bool runQueue;

  public List<GameObject> modules = new List<GameObject>();
  public Queue<ModuleAction> actionQueue = new Queue<ModuleAction>();

	// Use this for initialization
	void Start () {
    foreach (Transform child in transform) if (child.CompareTag("Module")) {
      modules.Add(child.gameObject);
    }
	}
	
	// Update is called once per frame
	void Update () {
	
	}

  public void Enqueue(ModuleAction action) {
    Debug.Log("enqueuing " + action.module.name);
    actionQueue.Enqueue(action);
  }
}
