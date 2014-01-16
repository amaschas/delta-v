using System.Collections;

class ModuleActionArgs : EventArgs {

	float startTimeOffset;

	// We can call this in derived classes via
	// public <ModuleType>ActionArgs(float startTimeOffset, <derived class args>) : base(float startTimeOffset)
	public ModuleActionArgs (float startTimeOffset) {
		this.startTimeOffset = startTimeOffset;
	}
}