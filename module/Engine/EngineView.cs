using UnityEngine;
using System.Collections;

public class EngineView : ModuleView {

  private Engine engine;
  private EngineController engineController;

	void Start () {
    engine = gameObject.GetComponent<Engine>();
    engineController = gameObject.GetComponent<EngineController>();
	}
	
  // If we send the action via event to the module controller we don't even need to get the other two gameobjects here
  // We could also have events fire when the engine starts or stops, which the module view listens to, and turns on and off the engine effects
	void OnGUI () {
    // engine.thrustDuration = GUI.HorizontalSlider(new Rect(105, Screen.height - 20, 100, 20), engine.thrustDuration, 0, 7);
    // This needs to return a module action to the shipController
	}
}
