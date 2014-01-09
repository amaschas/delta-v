using UnityEngine;
using System.Collections;

// We're now using ModuleActionInterface instead of this

// public class ModuleAction {
//   public ModuleControllerInterface module;
//   public Vector3 target;
//   public Quaternion rotationTarget;
//   public float duration;

//   public ModuleAction (ModuleControllerInterface module, Vector3 target, float duration) {
//     this.module = module;
//     this.target = target;
//     this.duration = duration;
//   }

//   public ModuleAction (ModuleControllerInterface module, Quaternion rotationTarget, float duration) {
//     this.module = module;
//     this.duration = duration;
//     this.rotationTarget = rotationTarget;
//   }

//   public void DoAction() {
//     module.Run(this);
//   }
// }

public class ModuleAction {
  public ModuleControllerInterface module;

  public ModuleAction (ModuleControllerInterface module, Vector3 target, float duration) {
    this.module = module;
  }
}