using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class EngineAction : ModuleAction {

	public float duration;

	public void EngineAction( ModuleInterface moduleController, float duration );

	void Start () {

		// Calculate heat generation for thrust duration
		heatGeneration = duration * HeatRate();
	}

}