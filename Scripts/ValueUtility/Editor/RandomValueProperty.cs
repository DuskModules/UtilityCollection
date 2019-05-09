#if UNITY_EDITOR
using UnityEditor;
using UnityEngine;

namespace DuskModules.DuskEditor {

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(RandomFloat))]
  public class RandomFloatProperty : BaseRandomProperty {

  }

  /// <summary> Custom property drawer for the MinMaxInt field </summary>
  [CustomPropertyDrawer(typeof(RandomInt))]
  public class RandomIntProperty : BaseRandomProperty {

  }

}
#endif