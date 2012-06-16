using UnityEngine;
using System.Collections;

public class ModuleAction {
  public ModuleInterface module;
  public Vector3 target;
  public Quaternion rotationTarget;
  public float duration;

  public ModuleAction (ModuleInterface module, Vector3 target, float duration) {
    this.module = module;
    this.target = target;
    this.duration = duration;
  }

  public ModuleAction (ModuleInterface module, Quaternion rotationTarget, float duration) {
    this.module = module;
    this.duration = duration;
    this.rotationTarget = rotationTarget;
  }

  public void DoAction() {
    module.Run(this);
  }
}