#if UNITY_EDITOR
using System.Reflection;
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {

	/// <summary> Draws the GetSet thing </summary>
	[CustomPropertyDrawer(typeof(GetSetAttribute))]
	sealed class GetSetDrawer : PropertyDrawer {
		public override void OnGUI(Rect position, SerializedProperty property, GUIContent label) {
			GetSetAttribute attribute = (GetSetAttribute)base.attribute;

			EditorGUI.BeginChangeCheck();
			EditorGUI.PropertyField(position, property, label);

			if (EditorGUI.EndChangeCheck()) {
				attribute.dirty = true;
			} else if (attribute.dirty) {
				object parent = GetParentObject(property.propertyPath, property.serializedObject.targetObject);

				System.Type type = parent.GetType();
				PropertyInfo info = type.GetProperty(attribute.name);

				if (info == null)
					Debug.LogError("Invalid property name \"" + attribute.name + "\"");
				else
					info.SetValue(parent, fieldInfo.GetValue(parent), null);

				attribute.dirty = false;
			}
		}

		public static object GetParentObject(string path, object obj) {
			string[] fields = path.Split('.');

			if (fields.Length == 1)
				return obj;

			FieldInfo info = obj.GetType().GetField(fields[0], BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
			obj = info.GetValue(obj);

			return GetParentObject(string.Join(".", fields, 1, fields.Length - 1), obj);
		}
	}
}
#endif