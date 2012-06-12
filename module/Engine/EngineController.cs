using UnityEngine;
using System.Collections;

public class EngineController : MonoBehaviour, ModuleActions {

  private Engine engine;
  private EngineView engineView;

	// Use this for initialization
	void Start () {
    engineView = gameObject.GetComponent<EngineView>();
    engine = gameObject.GetComponent<Engine>();
    engineView.enabled = false;
    engine.thrustDuration = 0.0f;
    engine.isRunning = false;
	}

  void Update () {
    // if(engine.thrustDuration > 0) {
    //   Debug.Log(engine.thrustDuration);
    // }
  }

  public void ActivateView () {
    engineView.enabled = true;
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
    ModuleAction action = new ModuleAction(gameObject, Vector3.zero, engine.thrustDuration);
    engine.thrustDuration = 0;
    return action;
  }
  
  public void DoAction (ModuleAction action) {
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
