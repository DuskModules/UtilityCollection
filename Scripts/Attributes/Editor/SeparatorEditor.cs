#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {

	/// <summary> Drawer for a seperator in editor window </summary>
	[CustomPropertyDrawer(typeof(SeparatorAttribute))]
	public class SeparatorEditor : DecoratorDrawer {

		SeparatorAttribute separatorAttribute => (SeparatorAttribute)attribute;

		public override void OnGUI(Rect position) {
			if (separatorAttribute.title == "") {
				position.height = 1;
				position.y += 14;
				GUI.Box(position, "");
			} else {
				GUIStyle style = EditorStyles.boldLabel;
				style.fontStyle = FontStyle.Bold;

				Vector2 textSize = style.CalcSize(new GUIContent(separatorAttribute.title));
				float separatorWidth = (position.width - textSize.x) / 2.0f - 5.0f;
				position.y += 14;

				GUI.Box(new Rect(position.xMin, position.yMin, separatorWidth, 1), "");
				GUI.Label(new Rect(position.xMin + separatorWidth + 5.0f, position.yMin - 8.0f, textSize.x, 20), separatorAttribute.title, style);
				GUI.Box(new Rect(position.xMin + separatorWidth + 10.0f + textSize.x, position.yMin, separatorWidth, 1), "");
			}
		}

		public override float GetHeight() {
			return 24.0f;
		}
	}
}
#endif