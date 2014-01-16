using System.Collections;

public class EngineActionArgs : ModuleActionArgs {

	public float duration;

	public EngineActionArgs(float startTimeOffset, float duration) : base(float startTimeOffset) {
		this.duration = duration;
	}

}