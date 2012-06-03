using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Ship : MonoBehaviour {

	public int actionQueueWidth;
	public int heatDissipation;
	public float rotationSpeed;
	public Material lineMaterial;

	private List<GameObject> modules;
	private List<ModuleAction> actionQueue;

    void Start() { Init(); }
    void OnEnable() { Init(); }

	public struct ModuleAction {
		private GameObject module;
		private float duration;
		private Vector3 target;
	}

	public void Init () {
		//Initialize modules with array of module GameObjects;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public ModuleAction NextInQueue () {
		//Returns next ModuleAction in actionQueue
		int length = actionQueue.Count;
		if(length > 0) {
			return actionQueue[length - 1];
		}
		else return default(ModuleAction);
	}
	
	public void AddToQueue (ModuleAction action) {
		//Add a ModuleAction to the end of the queue;
		actionQueue.Add(action);
	}
}
