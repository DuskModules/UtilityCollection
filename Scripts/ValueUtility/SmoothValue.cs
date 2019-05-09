using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> Value that smoothly matches the target value </summary>
  [System.Serializable]
  public class SmoothValue {

    /// <summary> If using constant, don't update. </summary>
    public bool useConstant = true;

    /// <summary> Current value </summary>
    public float value;
    /// <summary> Target value </summary>
    public float valueTarget;
    /// <summary> Speed to move with. </summary>
    [Tooltip("Speed with which to move the smooth value")]
    public LerpMoveValue speed;

    /// <summary> Updating of value </summary>
    /// <param name="time"> How much time has passed in seconds </param>
    public virtual void Update(float time = -1) {
      if (time < 0) time = Time.deltaTime;
      if (useConstant || value == valueTarget) return;
			value = speed.Move(value, valueTarget, time);
    }

		/// <summary> Basic constructor </summary>
		public SmoothValue() { }

		/// <summary> Setup the smooth value </summary>
		public SmoothValue(float value, LerpMoveValue speed) {
			this.value = value;
			valueTarget = value;
			this.speed = speed;
		}

    /// <summary> Copies the values of the target </summary>
    /// <param name="target"> The target to copy </param>
    public virtual void Copy(SmoothValue target) {
			useConstant = target.useConstant;
			value = target.value;
			valueTarget = target.valueTarget;
			speed.Copy(target.speed);
    }
  }
}