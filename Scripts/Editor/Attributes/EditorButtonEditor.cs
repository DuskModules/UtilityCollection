﻿using System;
using System.Linq;
using UnityEngine;
using UnityEditor;
using System.Reflection;
using System.Collections.Generic;

namespace DuskModules.DuskEditor {

	/// <summary> Custom editor for button </summary>
	[CanEditMultipleObjects]
	[CustomEditor(typeof(MonoBehaviour), true)]
	public class EditorButton : Editor {

		public override void OnInspectorGUI() {
			base.OnInspectorGUI();

			MonoBehaviour mono = (MonoBehaviour)target;
			IEnumerable<MemberInfo> methods = mono.GetType().
					GetMembers(BindingFlags.Instance | BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic).
					Where(o => Attribute.IsDefined(o, typeof(EditorButtonAttribute)));

			if (methods.Count() > 0) {
				GUILayout.Space(12);
			}

			foreach (MemberInfo memberInfo in methods) {
				if (GUILayout.Button(memberInfo.Name)) {
					MethodInfo method = (MethodInfo)memberInfo;
					method.Invoke(mono, null);
				}
			}
		}

	}
}