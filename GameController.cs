using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameController : MonoBehaviour {

  public bool turnPlaying = false;
  private ShipController selected;
  public Dictionary<string, ShipInterface> ships = new Dictionary<string, ShipInterface>();
  // public List<GameObject> ships = new List<GameObject>();

  public void Start () {
    InitShips();
  }

  public void InitShips () {
    // Check garbage collection
    GameObject[] shipObjects = GameObject.FindGameObjectsWithTag("Ship");
    foreach(GameObject ship in shipObjects) {
      ShipInterface controller = ship.GetComponent(typeof(ShipInterface)) as ShipInterface;
      ships.Add(ship.name, controller);
    }
  }

	public void SelectShip (GameObject UISelected) {
    Debug.Log(UISelected.name);
		if(selected) {
      selected.Deselect();
    }
    selected = UISelected.GetComponent<ShipController>();
		selected.Select();
    Camera.main.GetComponent<MouseScrollPanZoom>().target = UISelected.transform;
	}

}