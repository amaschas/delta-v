using UnityEngine;
using System.Collections;

public class EngineView : MonoBehaviour {

  private Engine engine;
  private EngineController engineController;

	void Start () {
    engine = gameObject.GetComponent<Engine>();
    engineController = gameObject.GetComponent<EngineController>();
	}
	
	void OnGUI () {
    engine.thrustDuration = GUI.HorizontalSlider(new Rect(105, Screen.height - 20, 100, 20), engine.thrustDuration, 0, 7);
	}
}
