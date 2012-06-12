using UnityEngine;
using System.Collections;

public class GameView : MonoBehaviour {

  public Material lineMaterial;

  private GameController gameController;

  // Use this for initialization
  void Start () {
    gameController = GameObject.Find("GameController").GetComponent<GameController>();
  }
  
  void OnGUI () {
  }

  // Update is called once per frame
  void Update () {
    if(Input.GetMouseButtonDown(0)) {
      RaycastHit hit = new RaycastHit();
      Ray ray =  new Ray();
      ray = Camera.main.ScreenPointToRay(Input.mousePosition);
      if (Physics.Raycast (ray, out hit)) {
        gameController.SelectShip(hit.transform.gameObject);
      }
    }
  }
}
