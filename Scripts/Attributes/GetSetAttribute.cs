using UnityEngine;

namespace DuskModules {

	/// <summary> Attribute to bind get setters </summary>
	public sealed class GetSetAttribute : PropertyAttribute {

		public readonly string name;
		public bool dirty;

		public GetSetAttribute(string name) {
			this.name = name;
		}

	}
}