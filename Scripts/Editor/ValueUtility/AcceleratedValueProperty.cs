using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {

  /// <summary> Custom property drawer for the accelerated field </summary>
  [CustomPropertyDrawer(typeof(AcceleratedValue))]
  public class AcceleratedValueProperty : PropertyDrawer {

    /// <summary> Called when it needs to draw on the GUI  </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      ExtraEditorUtility.BeginProperty(position, GUIContent.none, property);

      property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, 40, position.height), property.isExpanded, label, true);
      if (property.isExpanded) {
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("acceleration"), new GUIContent("Acceleration", "Speed gain per second towards target value."));
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("drag"), new GUIContent("Drag", "Exponentially decreases speed. The higher it is, the faster it decays."));
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("linearDrag"), new GUIContent("Linear Drag", "Linearly decreases speed. Constant decay."));
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("linearSpeed"), new GUIContent("Linear Speed", "Linear speed. Value always moves this much per second to target. " +
          "A small amount ensures the value snaps up and reaches the target."));
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("valueTarget"), new GUIContent("Target", "Target value, what the value should be."));
        ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("value"), new GUIContent("Value", "Current value, which moves to match target value."));

        SerializedProperty minLimit = property.FindPropertyRelative("hasMinimumLimit");
        ExtraEditorUtility.PropertyFieldLine(minLimit, new GUIContent("Has Minimum Limit", "Whether the value has a hard minimum limit. If reached, speed instantly stops."));
        if (minLimit.boolValue) {
          ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("minimumLimit"), new GUIContent("Minimum limit", "The actual minimum limit."));
        }

        SerializedProperty maxLimit = property.FindPropertyRelative("hasMaximumLimit");
        ExtraEditorUtility.PropertyFieldLine(maxLimit, new GUIContent("Has Maximum Limit", "Whether the value has a hard maximum limit. If reached, speed instantly stops."));
        if (maxLimit.boolValue) {
          ExtraEditorUtility.PropertyFieldLine(property.FindPropertyRelative("maximumLimit"), new GUIContent("Maximum limit", "The actual maximum limit."));
        }
      }

      EditorGUI.EndProperty();
    }

    /// <summary> Height of this property. </summary>
    public override float GetPropertyHeight(SerializedProperty property, GUIContent label) {
      int length = 9;
      if (property.FindPropertyRelative("hasMinimumLimit").boolValue) length++;
      if (property.FindPropertyRelative("hasMaximumLimit").boolValue) length++;
      return EditorGUIUtility.singleLineHeight * (property.isExpanded ? length : 1);
    }
  }
}