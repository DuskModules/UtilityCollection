using System;

/// <summary> Static class containing extension methods for angular calculations </summary>
public static class ActionExtensionMethods {
    
  /// <summary> Adds an action to this action. </summary>
  public static Action Add(this Action action, params Action[] others) {
    for (int i = 0; i < others.Length; i++) {
      action += others[i];
    }
    return action;
  }

  /// <summary> Adds an action to this action. </summary>
  public static Action<T> Add<T>(this Action<T> action, params Action<T>[] others) {
    for (int i = 0; i < others.Length; i++) {
      action += others[i];
    }
    return action;
  }

  /// <summary> Adds an action to this action. </summary>
  public static Action<T1, T2> Add<T1, T2>(this Action<T1, T2> action, params Action<T1, T2>[] others) {
    for (int i = 0; i < others.Length; i++) {
      action += others[i];
    }
    return action;
  }

}