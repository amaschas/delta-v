using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ShipView : MonoBehaviour {

  private ShipController shipController;
  private Ship ship;
  public List<GameObject> moduleViewList;
  public Queue<ModuleAction> actionQueueViewList;

	// Use this for initialization
	void Start () {
    shipController = GetComponent<ShipController>();
    ship = GetComponent<Ship>();
    moduleViewList = ship.modules;
    actionQueueViewList = ship.actionQueue;
	}

  void OnGUI () {
    GUI.Box (new Rect (Screen.width - 105,5,100,400), "Modules");
    int distanceFromTop = 30;
    foreach (GameObject module in ship.modules) {
      if(module.name != "Orientation") {
        if(GUI.Button(new Rect(Screen.width - 95,distanceFromTop,80,20), module.name)) {
          shipController.ActivateModule(module);
        }
        distanceFromTop += 25;
      }
    }
    distanceFromTop = 30;
    GUI.Box (new Rect (5,5,100,400), "Actions");
    foreach (ModuleAction action in ship.actionQueue) {
      // Debug.Log(action.duration);
      int actionDuration = (int) action.duration;
      if(GUI.Button(new Rect(15,distanceFromTop, 80, actionDuration * 10), action.module.name)) {
        Debug.Log(action.module.name);
      }
      distanceFromTop += actionDuration * 10 + 5;
    }
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 50, 100, 20), "Queue Action")) {
      shipController.AddCurrentActionToQueue();
    }
    if(GUI.Button(new Rect(Screen.width - 105, Screen.height - 25, 100, 20), "Play Actions")) {
      shipController.RunQueue();
    }
  }
}
