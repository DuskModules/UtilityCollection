#if UNITY_EDITOR
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {
	
	/// <summary> Custom property drawer for the ColorSetting field </summary>
	[CustomPropertyDrawer(typeof(ColorSetting))]
	public class ColorSettingEditor : PropertyDrawer {

		/// <summary> Called when it needs to draw on the GUI  </summary>
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			float labelWidth = EditorGUIUtility.labelWidth;

			ExtraEditorUtility.BeginProperty(position, label, property);

			ExtraEditorUtility.TakeSpace(80);
			SerializedProperty enumProp = property.FindPropertyRelative("colorUse");
			ExtraEditorUtility.PropertyField(enumProp);

			if (enumProp.enumValueIndex != 1) {
				ExtraEditorUtility.TakeSpace(4);
				ExtraEditorUtility.TakeSpace();
				ExtraEditorUtility.PropertyField(property.FindPropertyRelative("color"));
				
				EditorGUI.indentLevel++;
				Rect usePosition = new Rect(ExtraEditorUtility.totalPosition);

				usePosition.y += EditorGUIUtility.singleLineHeight;
				usePosition.height = EditorGUIUtility.singleLineHeight;

				EditorGUIUtility.labelWidth = labelWidth;
				EditorGUI.PropertyField(usePosition, property.FindPropertyRelative("speed"), new GUIContent("Speed"));
				EditorGUI.indentLevel--;
			}

			ExtraEditorUtility.EndProperty();
		}

		/// <summary> Height of this property. </summary>
		public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
			SerializedProperty enumProp = property.FindPropertyRelative("colorUse");
			return EditorGUIUtility.singleLineHeight * (enumProp.enumValueIndex == 1 ? 1 : 2);
		}
	}

}
#endif