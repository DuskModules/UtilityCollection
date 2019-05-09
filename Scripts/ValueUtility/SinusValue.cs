using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> A value that moves with Math.Sin, using frequency and strength </summary>
  [System.Serializable]
  public class SinusValue {

    /// <summary> Smooth matching value for frequency </summary>
    [Tooltip("Frequency of Sinus wave. Increase this value to make the value wobble between high and low faster.")]
    public SmoothValue frequency = new SmoothValue();
    /// <summary> Smooth matching value for strength </summary>
    [Tooltip("Strength of Sinus wave. Increase this value to make the value wobble to higher and lower extremes.")]
    public SmoothValue strength = new SmoothValue();

    /// <summary> Current X of sinus value </summary>
    protected float _x;
    /// <summary> Current X of sinus value </summary>
    public float x {
			get => _x;
			set => _x = value;
		}
		/// <summary> Resulting value </summary>
		protected float _value;
    /// <summary> Resulting value </summary </summary>
    public float value {
			get => _value;
			set => _value = value;
		}

		/// <summary> Constructor when made behind the scenes </summary>
		public SinusValue() {
			frequency = new SmoothValue();
			strength = new SmoothValue();
		}

		/// <summary> Constructor when made behind the scenes </summary>
		public SinusValue(LerpMoveValue frequencySpeed, LerpMoveValue strengthSpeed) {
			frequency = new SmoothValue { useConstant = false };
			strength = new SmoothValue { useConstant = false };
			frequency.speed = frequencySpeed;
			strength.speed = strengthSpeed;
		}

		/// <summary> Resets x to 0. </summary>
		public void Reset() {
			x = 0;
    }
    /// <summary> Resets x to a random value </summary>
    public void RandomReset() {
			x = Random.Range(0, Mathf.PI * 2);
    }

    /// <summary> Updating of value </summary>
    /// <param name="time"> How much time has passed in seconds </param>
    public void Update(float time = -1) {
      if (time < 0) time = Time.deltaTime;
      float fr = frequency.value;
      float st = strength.value;
			frequency.Update(time);
			strength.Update(time);

      if (fr != frequency.value || frequency.value > 0)
				x += frequency.value * time;
      if (st != strength.value || strength.value > 0)
				value = Mathf.Sin(x) * strength.value;
    }

    /// <summary> Copies the values of the target </summary>
    /// <param name="target"> The target to copy </param>
    public void Copy(SinusValue target) {
			frequency.Copy(target.frequency);
			strength.Copy(target.strength);

			x = target.x;
			value = target.value;
    }
  }
}