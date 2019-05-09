using DuskModules;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> A Minimimum / Maximum value that can generate a random int value. </summary>
  [System.Serializable]
  public class RandomInt {

    /// <summary> Whether to use the constant (minimum) or not </summary>
    public bool useConstant = true;
    /// <summary> Constant value </summary>
    public int constant;

    /// <summary> The minimum possible value </summary>
    public int minimum;
    /// <summary> The maximum possible value </summary>
    public int maximum;
    /// <summary> The actual current value </summary>
    public int value {
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
    public int lastValue { get; protected set; }
    
    /// <summary> Copies the target values </summary>
    /// <param name="target"> Target to copy </param>
    public void Copy(RandomInt target) {
			useConstant = target.useConstant;
			constant = target.constant;
			minimum = target.minimum;
			maximum = target.maximum;
    }
  }
}