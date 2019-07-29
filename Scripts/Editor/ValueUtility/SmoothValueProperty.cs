using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace DuskModules.DuskEditor {

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(SmoothValue))]
  public class SmoothValueProperty : BaseSmoothValueProperty {
    
  }

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(SmoothVector2))]
  public class SmoothVector2Property : BaseSmoothValueProperty {

  }

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(SmoothVector3))]
  public class SmoothVector3Property : BaseSmoothValueProperty {

  }

  /// <summary> Custom property drawer for the MinMaxFloat field </summary>
  [CustomPropertyDrawer(typeof(SmoothQuaternion))]
  public class SmoothQuaternionProperty : BaseSmoothValueProperty {

    /// <summary> Whether to show the actual values. </summary>
    protected override bool ShowValues() {
      return false;
    }

  }
}