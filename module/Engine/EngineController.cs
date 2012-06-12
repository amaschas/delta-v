using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineController : MonoBehaviour, ModuleInterface {

  private Engine engine;
  private EngineView engineView;
  public Dictionary <string, ModuleInterface> shipModules;

	void Start () {
    shipModules = transform.parent.gameObject.GetComponent<ShipController>().modules;
    engineView = gameObject.GetComponent<EngineView>();
    engine = gameObject.GetComponent<Engine>();
    engineView.enabled = false;
    engine.thrustDuration = 0.0f;
    engine.isRunning = false;
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

  public bool HasAction () {
    // if(engine.thrustDuration > 0) {
      return true;
    // }
    // else return false;
  }
  
  public ModuleAction GetAction () {
    ModuleAction action = new ModuleAction(this, Vector3.zero, engine.thrustDuration);
    engine.thrustDuration = 0;
    return action;
  }
  
  public void Run (ModuleAction action) {
    if(!engine.isRunning) {
      engine.isRunning = true;
      engine.thrustDuration = action.duration;
    }
    if(engine.thrustDuration <= 0) {
      engine.isRunning = false;
    }
    transform.parent.gameObject.GetComponent<ShipController>().AddThrust(engine.thrust);
    Debug.Log(engine.thrustDuration);
    Debug.Log(Time.deltaTime);
    engine.thrustDuration -= Time.deltaTime;
  }
  
  public bool IsRunning () {
    return engine.isRunning;
  }
}
