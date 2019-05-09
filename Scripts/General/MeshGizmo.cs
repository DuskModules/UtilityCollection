using System.Collections;
using System.Collections.Generic;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
#endif

namespace DuskModules {

	/// <summary> Hides the world collision </summary>
	public class MeshGizmo : MonoBehaviour {

		/// <summary> Mesh type </summary>
		public enum MeshType {
			surface,
			obstacle,
			trigger,
			area
		}
		public MeshType type;

#if UNITY_EDITOR
		/// <summary> Draws mesh gizmo </summary>
		private void OnDrawGizmos() {
			Gizmos.color = Color.white;

			switch (type) {
				case MeshType.surface: Gizmos.color = new Color32(0, 40, 255, 128); break;
				case MeshType.obstacle: Gizmos.color = new Color32(255, 0, 0, 128); break;
				case MeshType.trigger: Gizmos.color = new Color32(255, 128, 0, 128); break;
				case MeshType.area: Gizmos.color = new Color32(255, 255, 0, 64); break;
			}

			MeshFilter mesh = gameObject.GetComponent<MeshFilter>();
			Gizmos.DrawWireMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.lossyScale);
			Gizmos.color = Gizmos.color.WithA(Gizmos.color.a / 2);
			Gizmos.DrawMesh(mesh.sharedMesh, transform.position, transform.rotation, transform.lossyScale);

		}
#endif

	}
}