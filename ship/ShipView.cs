using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipView : MonoBehaviour {

  private ShipControllerInterface shipController;

	void Start () {
    shipController = GetComponent<ShipControllerInterface>();
	}

  void Update () {
  }

  void OnGUI () {
    GUI.Box (new Rect (Screen.width - 105,5,100,400), "Modules");
    int distanceFromTop = 30;
    foreach (KeyValuePair<string, ModuleControllerInterface> pair in shipController.modules) {
      if(pair.Key != "Orientation") {
        if(GUI.Button(new Rect(Screen.width - 95,distanceFromTop,80,20), pair.Key)) {
          shipController.ActivateModule(pair.Value);
        }
        distanceFromTop += 25;
      }
    }
  }
}
