using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineController : MonoBehaviour, ModuleInterface {

	private Engine engine;
	private EngineView engineView;

	void Start () {
		engineView = gameObject.GetComponent<EngineView>();
		engine = gameObject.GetComponent<Engine>();
		engineView.enabled = false;
	}

	public string Name () {
		return transform.name;
	}

	public ModuleInterface ActivateView () {
		engineView.enabled = true;
		return this;
	}

	public void DeactivateView () {
		engineView.enabled = false;
	}

	public void Run (ModuleAction action) {
		// This needs to happen in a fixed update, maybe kill this run structure entirely, and just run fixedupdate in each module?
		// or maybe it makes sense for each ship to have one fixed update call that triggers each discrete module action
		transform.parent.gameObject.GetComponent<ShipController>().AddThrust(engine.thrust * time.fixedDeltaTime);
	}
	
}
