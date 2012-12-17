using UnityEngine;
using System;
using System.Collections;

public class Module : MonoBehaviour {
  public int HeatCost;
  public int actionCost;
  public float durationModifier;

  // New Messaging stuff
  // *******************
  public delegate void ModuleStateChangeHandler(GameObject e);
  public event ModuleStateChangeHandler StateChange;

  public void OnStateChange() {
    if(StateChange != null)
      StateChange(this.gameObject);
  }
}
