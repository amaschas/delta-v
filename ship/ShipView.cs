using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipView : MonoBehaviour {

  private ShipController shipController;
  // public Queue<ModuleAction> actionQueueViewList;

	void Start () {
    shipController = GetComponent<ShipController>();
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
    // Move this into individual modules
    // distanceFromTop = 30;
    // GUI.Box (new Rect (5,5,100,400), "Actions");
    // foreach (ModuleAction action in shipController.actionQueue) {
    //   // Debug.Log(action.module.name);
    //   int actionDuration = (int) action.duration;
    //   if(GUI.Button(new Rect(15,distanceFromTop, 80, actionDuration * 10), action.module.name)) {
    //     Debug.Log(action.module.name);
    //   }
    //   distanceFromTop += actionDuration * 10 + 5;
    // }
    // if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 50, 100, 20), "Queue Action")) {
    //   // This button should go in the parent ModuleView class
    //   shipController.AddCurrentActionToQueue();
    // }
  }
}
