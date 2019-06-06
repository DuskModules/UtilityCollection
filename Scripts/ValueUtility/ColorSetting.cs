using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

	/// <summary> Color setting to act as a target color for graphics </summary>
	[System.Serializable]
	public class ColorSetting {

		/// <summary> What part of the color is used </summary>
		public enum ColorUse {
			full,
			nothing,
			colorOnly,
			alphaOnly
		}

		/// <summary> Color of the setting </summary>
		public Color color;
		/// <summary> What part of the color is used </summary>
		public ColorUse colorUse;

		/// <summary> How much of this color is used when combining </summary>
		public SmoothValue fill { get; private set; }
		/// <summary> Speed of filling color </summary>
		public LerpMoveValue speed;

		/// <summary> When used for first time </summary>
		public ColorSetting() {
			fill = new SmoothValue(0, speed);
		}

		/// <summary> Updating of value </summary>
		/// <param name="time"> How much time has passed in seconds </param>
		public virtual void Update(float time = -1) {
			if (time < 0) time = Time.deltaTime;
			fill.speed = speed;
			fill.Update(time);
		}

		/// <summary> Static method to combine multiple colors together. </summary>
		public static Color Combine(params ColorSetting[] colors) {
			return Combine(Color.white, colors);
		}
		
		/// <summary> Static method to combine multiple colors together. </summary>
		public static Color Combine(Color baseColor, params ColorSetting[] colors) {
			float a = baseColor.a;
			for (int i = 0; i < colors.Length; i++) {
				if (colors[i].colorUse == ColorUse.full || colors[i].colorUse == ColorUse.colorOnly)
					baseColor += colors[i].color * colors[i].fill.value;
				if (colors[i].colorUse == ColorUse.full || colors[i].colorUse == ColorUse.alphaOnly)
					a += colors[i].color.a * colors[i].fill.value;
			}
			return baseColor.WithA(a);
		}
	}
	
}