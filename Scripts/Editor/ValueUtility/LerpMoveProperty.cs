using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(LerpMoveValue))]
  public class LerpMoveProperty : PropertyDrawer {

    /// <summary> Called when it needs to draw on the GUI  </summary>
    public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
      ExtraEditorUtility.BeginProperty(position, label, property);
      
      ExtraEditorUtility.TakeSpace(32);
      SerializedProperty enumProp = property.FindPropertyRelative("mode");
      ExtraEditorUtility.DrawSettingsEnumDropdown(ExtraEditorUtility.contentPosition, enumProp);
      
      ExtraEditorUtility.TakeSpace();
      if (enumProp.enumValueIndex == 1 || enumProp.enumValueIndex == 0)
        ExtraEditorUtility.AddPropertyStack(property.FindPropertyRelative("lerpSpeed"), new GUIContent("L", "Speed to lerp values with."));
      if (enumProp.enumValueIndex == 2 || enumProp.enumValueIndex == 0)
        ExtraEditorUtility.AddPropertyStack(property.FindPropertyRelative("moveSpeed"), new GUIContent("M", "Speed to move values with."));
      ExtraEditorUtility.DrawPropertyStack(16);
      
      ExtraEditorUtility.EndProperty();
    }
  }
}