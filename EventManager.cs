// using UnityEngine;
using System.Collections;

// Good use case for a global singleton here

// Utility class for raising events
public class EventManager {

	// Dict of all events by broadcaster (this might only work with an event manager per-ship, else the dict gets huge)
	protected Dictionary<string, Dictionary<<string, EventHandler>> EventBroadcasters;

	// Register all module events with EventManager
	public void RegisterEvents( ModuleControllerInterface moduleController ){

	}

	// Register an event with EventManager
	protected void RegisterEvent( string eventBroadcaster, EventHandler event){

	}

	// Listen to an object and an event
	public void SetupListener( ModuleControllerInterface listener, string broadcaster, string event ){

	}

	// will EventArgs work here polymorphically?
	public static void RaiseEvent ( EventHandler eventHandler, EventArgs args = EventArgs.Empty ) {

		// Make a copy to be thread safe
		EventHandler handler = eventHandler;

		// Raise the event if not null
		if (handler != null) {
			handler(this,  args);
		}
	}

}