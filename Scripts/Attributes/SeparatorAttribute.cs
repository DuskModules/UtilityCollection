using UnityEngine;

namespace DuskModules {

	/// <summary> Attribute for a seperator in editor window </summary>
	public class SeparatorAttribute : PropertyAttribute {

		public readonly string title;

		public SeparatorAttribute() {
			title = "";
		}
		public SeparatorAttribute(string title) {
			this.title = title;
		}

	}
}