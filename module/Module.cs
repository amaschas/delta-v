using UnityEngine;
using System.Collections;

public struct ModuleAction {
  public GameObject module;
  public Vector3 target;
  public float duration;

  public ModuleAction (GameObject module, Vector3 target, float duration) {
    this.module = module;
    this.target = target;
    this.duration = duration;
  }
}

public interface ModuleActions {
  void ActivateView ();
  void DeactivateView ();
  bool HasAction ();
  ModuleAction GetAction ();
  void DoAction (ModuleAction action);
  bool IsRunning ();
  // Meed method to get current state of module to action queue
}

public class Module : MonoBehaviour {
  public int HeatCost;
  public int actionCost;
  public float durationModifier;
  public bool IsDoingAction;
}
