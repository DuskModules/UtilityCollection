#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;
using UnityEditorInternal;
using System.Reflection;
using System;
using System.Collections.Generic;

// Never an end-point, and thus not in Editor namespace. Can be used by other modules.
namespace DuskModules {

  /// <summary> Utility Script for Editor related code </summary>
  public class ExtraEditorUtility : MonoBehaviour {

    // ExtraEditorUtility provides methods to more easily write custom Editor code.
    // Especially with custom properties. Call BeginProperty to add an indentation to
    // the begin x position of the GUI. All other methods remember in what indentation
    // it is, and draw the correct layout accordingly.
    // See Editor scripts in the UtilityCollection for examples.

    //================================[ GUI Workspace Variables ]================================\\
    /// <summary> Total position of GUI call </summary>
    public static Rect totalPosition;
    /// <summary> Current working content position </summary>
    public static Rect contentPosition;
    /// <summary> Total width of property area </summary>
    public static float propertyWidth;
    /// <summary> Remaining width of property area </summary>
    public static float remainingWidth => propertyWidth - totalTakenSpace;
    /// <summary> How much space has been taken totally </summary>
    protected static float totalTakenSpace;
    /// <summary> How much space has been taken by previous set </summary>
    protected static float takenSpace;

    /// <summary> Draw stack of properties, collected to know the height of the total property </summary>
    protected static List<SerializedProperty> drawStackProperty;
    /// <summary> Draw stack of GUI Contents, being used for the properties </summary>
    protected static List<GUIContent> drawStackContent;

    /// <summary> Content position for lined properties </summary>
    protected static Rect linedContentPosition;


    //================================[ GUI Workspace Methods ]================================\\
    // The following methods maintain and update the GUI workspace, such as remembering what indentation
    // the GUI drawing code is currently at.

    /// <summary> Begins property for easier property drawing code </summary>
    public static void BeginProperty(Rect position, GUIContent label, SerializedProperty property, bool foldOut = false) {
      label = EditorGUI.BeginProperty(position, label, property);
      totalPosition = position;

      if (foldOut) {
        property.isExpanded = EditorGUI.Foldout(new Rect(position.x, position.y, EditorGUIUtility.labelWidth, position.height), property.isExpanded, label, true);
        label.text = " ";
      }

      contentPosition = EditorGUI.PrefixLabel(position, label);
      contentPosition.height = EditorGUIUtility.singleLineHeight;
      propertyWidth = contentPosition.width;
      takenSpace = 0;
      totalTakenSpace = 0;

      linedContentPosition = new Rect(totalPosition);
      linedContentPosition.height = EditorGUIUtility.singleLineHeight;
    }

    /// <summary> Ends property. </summary>
    public static void EndProperty() {
      EditorGUI.EndProperty();
      
      drawStackProperty = new List<SerializedProperty>();
      drawStackContent = new List<GUIContent>();
    }

    /// <summary> Starts the property area </summary>
    public static int ZeroIndent() {
      int indent = EditorGUI.indentLevel;
      EditorGUI.indentLevel = 0;
      return indent;
    }
    /// <summary> Ends the property area </summary>
    public static void ResetIndent(int indent) {
      EditorGUI.indentLevel = indent;
    }

    /// <summary> Takes up an amount of space from contentPosition. </summary>
    public static void TakeSpace(float space = 0) {
      if (space == 0) space = remainingWidth;
      contentPosition.x += takenSpace;
      contentPosition.width = space;
      takenSpace = space;
      totalTakenSpace += space;
    }

    
    //================================[ Drawers ]================================\\
    // The following methods all draw something within the editor GUI.

    /// <summary> Draws a GUI Layout line </summary>
    public static void LayoutLine() {
      GUILayout.Box("", new GUILayoutOption[] { GUILayout.ExpandWidth(true), GUILayout.Height(1) });
    }

    /// <summary> Draws a header label </summary>
    public static void HeaderField(string header) {
      EditorGUILayout.LabelField(header, EditorStyles.boldLabel);
    }
    
    /// <summary> Adds something to the property stack </summary>
    public static void AddPropertyStack(SerializedProperty property, GUIContent content) {
      if (drawStackProperty == null) drawStackProperty = new List<SerializedProperty>();
      if (drawStackContent == null) drawStackContent = new List<GUIContent>();

      drawStackProperty.Add(property);
      drawStackContent.Add(content);
    }
    /// <summary> Draws the property stack </summary>
    public static void DrawPropertyStack(float labelWidth) {
      int indent = ZeroIndent();

      EditorGUIUtility.labelWidth = labelWidth;
      Rect drawPosition = new Rect(contentPosition);
      drawPosition.width = contentPosition.width / drawStackProperty.Count;

      for (int i = 0; i < drawStackProperty.Count; i++) {
        EditorGUI.PropertyField(drawPosition, drawStackProperty[i], drawStackContent[i]);
        drawPosition.x += drawPosition.width;
      }

      ResetIndent(indent);

      drawStackProperty = new List<SerializedProperty>();
      drawStackContent = new List<GUIContent>();
    }

    /// <summary> Shorter version of property field, tied to this content position </summary>
    public static void PropertyField(SerializedProperty property, GUIContent content = null) {
      if (content == null) content = GUIContent.none;
      int indent = ZeroIndent();
      EditorGUI.PropertyField(contentPosition, property, content);
      ResetIndent(indent);
    }

    /// <summary> Shorter version of property field, tied to this content position </summary>
    public static void PropertyFieldLine(SerializedProperty property, GUIContent content = null) {
      if (content == null) content = GUIContent.none;
      EditorGUI.indentLevel++;
      linedContentPosition.y += EditorGUIUtility.singleLineHeight;
      EditorGUI.PropertyField(linedContentPosition, property, content);
      EditorGUI.indentLevel--;
    }

    /// <summary> Draws an settings-dropdown-button for a named boolean toggle. </summary>
    public static void DrawSettingsToggleDropdown(Rect contentPosition, SerializedProperty boolProperty, string trueName, string falseName) {
      int indent = ZeroIndent();

      GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButton);
      buttonStyle.normal.background = null;
      Texture2D buttonIcon = GUI.skin.GetStyle("Icon.TrackOptions").normal.background;

      if (EditorGUI.DropdownButton(contentPosition, new GUIContent(buttonIcon), FocusType.Passive, buttonStyle)) {
        GenericMenu settingsMenu = new GenericMenu();

        settingsMenu.AddItem(new GUIContent(trueName), boolProperty.boolValue, delegate (object o) {
          boolProperty.boolValue = true;
          boolProperty.serializedObject.ApplyModifiedProperties();
        }, 0);

        settingsMenu.AddItem(new GUIContent(falseName), !boolProperty.boolValue, delegate (object o) {
          boolProperty.boolValue = false;
          boolProperty.serializedObject.ApplyModifiedProperties();
        }, 1);

        settingsMenu.ShowAsContext();
      }

      ResetIndent(indent);
    }

    /// <summary> Draws an settings-dropdown-button for an enum dropdown. </summary>
    public static void DrawSettingsEnumDropdown(Rect contentPosition, SerializedProperty enumProperty) {
      int indent = ZeroIndent();

      GUIStyle buttonStyle = new GUIStyle(EditorStyles.miniButton);
      buttonStyle.normal.background = null;
      Texture2D buttonIcon = GUI.skin.GetStyle("Icon.TrackOptions").normal.background;

      if (EditorGUI.DropdownButton(contentPosition, new GUIContent(buttonIcon), FocusType.Passive, buttonStyle)) {
        GenericMenu settingsMenu = new GenericMenu();

        // Create setting for each enum name.
        for (int i = 0; i < enumProperty.enumDisplayNames.Length; i++) {
          int a = i;
          settingsMenu.AddItem(new GUIContent(enumProperty.enumDisplayNames[i]), enumProperty.enumValueIndex == i, delegate (object o) {
            enumProperty.enumValueIndex = a;
            enumProperty.serializedObject.ApplyModifiedProperties();
          }, i);
        }

        settingsMenu.ShowAsContext();
      }

      ResetIndent(indent);
    }


    //================================[ Utilities ]================================\\
    /// <summary> Gets an array of the available sorting layer names </summary>
    public static string[] GetSortingLayerNames() {
      Type internalEditorUtilityType = typeof(InternalEditorUtility);
      PropertyInfo sortingLayersProperty = internalEditorUtilityType.GetProperty("sortingLayerNames", BindingFlags.Static | BindingFlags.NonPublic);
      return (string[])sortingLayersProperty.GetValue(null, new object[0]);
    }

    /// <summary> Finds and returns a list of all scripts of a certain type. </summary>
    public static List<MonoScript> FindScriptsOfType<T>() {
      string[] guid = AssetDatabase.FindAssets("t:MonoScript");
      List<MonoScript> scripts = new List<MonoScript>();

      for (int i = 0; i < guid.Length; i++) {
        string path = AssetDatabase.GUIDToAssetPath(guid[i]);
        MonoScript sc = AssetDatabase.LoadAssetAtPath<MonoScript>(path);
        System.Type t = sc.GetClass();
        if (t != null && !t.IsAbstract && (t == typeof(T) || t.IsSubclassOf(typeof(T)))) {
          scripts.Add(sc);
        }
      }
      return scripts;
    }
    
    /// <summary> Converts the given name to an editor friendly name. </summary>
    public static string EditorFriendlyName(string name) {
      int i = 0;
      while (i < name.Length) {
        if (i > 0 && char.IsUpper(name[i])) {
          char prev = name[i - 1];
          if (char.IsLetterOrDigit(prev)) {
            name = name.Insert(i, " ");
            i++;
          }
        }
        i++;
      }
      return name;
    }
    
  }
}
#endif