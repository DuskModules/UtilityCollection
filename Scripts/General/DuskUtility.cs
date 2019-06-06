using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary> Class with generic utility methods </summary>
public static class DuskUtility {

	/// <summary> Returns unscaled delta time, but it cannot exceed the maximum particle deltaTime. </summary>
	public static float interfaceDeltaTime => Mathf.Min(Time.unscaledDeltaTime, Time.maximumParticleDeltaTime);

}