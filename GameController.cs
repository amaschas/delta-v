using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

  private ShipController selected;

	public void SelectShip (GameObject UISelected) {
    Debug.Log(UISelected.name);
		if(selected) {
      selected.Deselect();
    }
    selected = UISelected.GetComponent<ShipController>();
		selected.Select();
    // Camera.main.GetComponent<MaxCamera>().target = selected.transform;
	}

}