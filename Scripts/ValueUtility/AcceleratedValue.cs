using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> Value that smoothly matches the target value, using acceleration, speed and drag.
  /// This causes it to overshoot its target, resulting in wobbles and pop in movement. </summary>
  [System.Serializable]
  public class AcceleratedValue {
    
    public float acceleration = 300;
    public float drag = 6;
    public float linearDrag = 1;
    public float linearSpeed = 0.1f;
    
    public float valueTarget;
    public float value;
		
    public bool hasMinimumLimit;
    public float minimumLimit;
		
    public bool hasMaximumLimit;
    public float maximumLimit = 1;

    public float speed { get; private set; }
    
    /// <summary> Basic constructor </summary>
    public AcceleratedValue() { }

    /// <summary> Setup the accelerated value </summary>
    public AcceleratedValue(float value, float acceleration, float drag, float linearDrag, float linearSpeed) {
      this.value = value;
      valueTarget = value;
      this.acceleration = acceleration;
      this.drag = drag;
      this.linearDrag = linearDrag;
      this.linearSpeed = linearSpeed;
    }

    /// <summary> Sets a hard minimum limit </summary>
    public void SetMinimumLimit(float limit = 0) {
      hasMinimumLimit = true;
      minimumLimit = limit;
    }

    /// <summary> Sets a hard maximum limit </summary>
    public void SetMaximumLimit(float limit = 1) {
      hasMaximumLimit = true;
      maximumLimit = limit;
    }

		/// <summary> Sets the value directly, resetting speed. </summary>
		public virtual void SetValue(float value) {
			this.value = value;
			valueTarget = value;
			speed = 0;
		}

		/// <summary> Sets the target, keeping it within possible limits </summary>
		public virtual void SetTarget(float target) {
			valueTarget = target;
			if (hasMinimumLimit && valueTarget < minimumLimit)
				valueTarget = minimumLimit;
			if (hasMaximumLimit && valueTarget > maximumLimit)
				valueTarget = maximumLimit;
		}

		/// <summary> Updating of value </summary>
		/// <param name="time"> How much time has passed in seconds </param>
		public virtual void Update(float time = -1) {
      if (time < 0) time = Time.deltaTime;

      if (value != valueTarget || speed != 0) {
        float delta = valueTarget - value;
        float acc = delta * acceleration;
        
        speed += acc * time;
        speed *= Mathf.Pow(.1f, time * drag);
        if (linearDrag > 0)
          speed = Mathf.MoveTowards(speed, 0, linearDrag * time);

        value += speed * time;
        value = Mathf.MoveTowards(value, valueTarget, linearSpeed * time);

        if (hasMinimumLimit && value < minimumLimit) {
          value = minimumLimit;
          speed = 0;
        }
        else if (hasMaximumLimit && value > maximumLimit) {
          value = maximumLimit;
          speed = 0;
        }
      }
    }
		
    /// <summary> Copies the values of the target </summary>
    /// <param name="target"> The target to copy </param>
    public virtual void Copy(AcceleratedValue target) {
      acceleration = target.acceleration;
      drag = target.drag;
      linearDrag = target.linearDrag;
      linearSpeed = target.linearSpeed;
      valueTarget = target.valueTarget;
      value = target.value;
      speed = target.speed;
    }
  }
}