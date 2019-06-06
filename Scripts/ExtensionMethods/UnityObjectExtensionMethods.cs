using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using DuskModules;

/// <summary> Static Class containing extension methods for Unity Monobehaviours and objects </summary>
public static class UnityObjectExtensionMethods {

	/// <summary> Checks to see if given object is within layer mask. </summary>
	/// <param name="obj"> What object to check </param>
	/// <param name="mask"> What layermask </param>
	/// <returns> Whether the object is within the layermask or not </returns>
	public static bool IsInLayerMask(this GameObject obj, LayerMask mask) {
		return ((mask.value & (1 << obj.layer)) > 0);
	}

	/// <summary> Manually searches through the parents for the required component. </summary>
	/// <typeparam name="T"> Type to find </typeparam>
	/// <returns> Found component, if any </returns>
	public static T GetComponentInParentInactive<T>(this Behaviour behaviour) where T : Component {
		return GetComponentInParentInactive<T>(behaviour.gameObject);
	}

	/// <summary> Manually searches through the parents for the required component. </summary>
	/// <typeparam name="T"> Type to find </typeparam>
	/// <param name="obj"> Object to start with </param>
	/// <returns> Found component, if any </returns>
	public static T GetComponentInParentInactive<T>(this GameObject obj) where T : Component {
		Transform trf = obj.transform;
		while (trf != null) {
			T comp = trf.gameObject.GetComponent<T>();
			if (comp != null) return comp;
			trf = trf.parent;
		}
		return null;
	}

	/// <summary> Gets and returns a component, saving it in the set local variable </summary>
	/// <typeparam name="T"> Type of component to get (automatically set by compiler) </typeparam>
	/// <param name="gameObject"> GameObject to work with </param>
	/// <param name="local"> The found object </param>
	/// <returns> The found object </returns>
	public static T GetComponentCached<T>(this GameObject gameObject, ref T local) where T : Component {
		if (local == null) local = gameObject.GetComponent<T>();
		return local;
	}

	/// <summary> Gets and returns a component, saving it in the set local variable </summary>
	/// <typeparam name="T"> Type of component to get (automatically set by compiler) </typeparam>
	/// <param name="behaviour"> Behaviour to work with </param>
	/// <param name="local"> The found object </param>
	/// <returns> The found object </returns>
	public static T GetComponentCached<T>(this Behaviour behaviour, ref T local) where T : Component {
		if (local == null) local = behaviour.GetComponent<T>();
		return local;
	}

	/// <summary> Gets and returns a component, saving it in the set local variable </summary>
	/// <typeparam name="T"> Type of component to get (automatically set by compiler) </typeparam>
	/// <param name="gameObject"> GameObject to work with </param>
	/// <param name="local"> The found object </param>
	/// <returns> The found object </returns>
	public static T GetComponentInChildrenCached<T>(this GameObject gameObject, ref T local) where T : Component {
		if (local == null) local = gameObject.GetComponentInChildren<T>();
		return local;
	}

	/// <summary> Gets and returns a component, saving it in the set local variable </summary>
	/// <typeparam name="T"> Type of component to get (automatically set by compiler) </typeparam>
	/// <param name="behaviour"> Behaviour to work with </param>
	/// <param name="local"> The found object </param>
	/// <returns> The found object </returns>
	public static T GetComponentInChildrenCached<T>(this Behaviour behaviour, ref T local) where T : Component {
		if (local == null) local = behaviour.GetComponentInChildren<T>();
		return local;
	}

	/// <summary> Gets and returns a component in parent, saving it in the set local variable </summary>
	/// <typeparam name="T"> Type of component to get (automatically set by compiler) </typeparam>
	/// <param name="behaviour"> Behaviour to work with </param>
	/// <param name="local"> The found object </param>
	/// <returns> The found object </returns>
	public static T GetComponentInParentCached<T>(this Behaviour behaviour, ref T local) where T : Component {
		if (local == null) local = GetComponentInParentInactive<T>(behaviour);
		return local;
	}

  /// <summary> Gets the correct deltaTime for a certain time type. </summary>
  /// <typeparam name="T"> Type to find </typeparam>
  /// <param name="timeType"> Type of time to get </param>
  /// <returns> Timeframe </returns>
  public static float GetDeltaTime(this Behaviour behaviour, TimeType timeType) {
    switch (timeType) {
      case TimeType.deltaTime: return Time.deltaTime;
      case TimeType.unscaledDeltaTime: return DuskUtility.interfaceDeltaTime;
    }
    return Time.deltaTime;
  }

  /// <summary> Checks if given object is of the given type, or a subclass of </summary>
  /// <param name="obj"> What object to check </param>
  /// <param name="type"> What type to check for </param>
  /// <returns> True if it matches </returns>
  public static bool IsOfType(this object obj, System.Type type) {
		if (obj == null) return false;
		System.Type objType = obj.GetType();
		if (objType == type || objType.IsSubclassOf(type)) {
			return true;
		}
		return false;
	}

	/// <summary> Creates an exact duplicate of any serializable object, do not us on non-serializable objects. </summary>
	/// <typeparam name="T"> The object type </typeparam>
	/// <param name="obj"> The actual object </param>
	/// <returns> The copy of the object </returns>
	public static T DeepClone<T>(this T obj) {
		if (!typeof(T).IsSerializable) {
			Debug.LogWarning("The type must be serializable, " + obj.ToString() + " is not.");
			return default(T);
		}
		using (MemoryStream ms = new MemoryStream()) {
			BinaryFormatter formatter = new BinaryFormatter();
			formatter.Serialize(ms, obj);
			ms.Position = 0;
			return (T)formatter.Deserialize(ms);
		}
	}
}