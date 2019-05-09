using DuskModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> A Minimimum / Maximum value that can generate a random float value. </summary>
  [System.Serializable]
  public class RandomFloat {

    /// <summary> Whether to use the constant (minimum) or not </summary>
    public bool useConstant = true;
    /// <summary> Constant value </summary>
    public float constant;

    /// <summary> The minimum possible value </summary>
    public float minimum;
    /// <summary> The maximum possible value </summary>
    public float maximum;
    /// <summary> The actual current value </summary>
    public float value {
      get {
        if (useConstant) return constant;
        else {
					lastValue = Random.Range(minimum, maximum);
          return lastValue;
        }
      }
      set {
        if (useConstant) constant = value;
      }
    }
    /// <summary> Last value generated </summary>
    public float lastValue { get; protected set; }
    
    /// <summary> Copies the target values </summary>
    /// <param name="target"> Target to copy </param>
    public void Copy(RandomFloat target) {
			useConstant = target.useConstant;
			constant = target.constant;
			minimum = target.minimum;
			maximum = target.maximum;
    }
  }
}