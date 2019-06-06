using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace DuskModules {

  /// <summary> A value that moves down over time to zero. Upon reaching zero, it completes. </summary>
  [System.Serializable]
  public class TimerValue {

    /// <summary> Called when timer is completed </summary>
    protected Action onCompleteCallback;

    /// <summary> Total time for timer </summary>
    public float time = 0;
    /// <summary> Speed of timer </summary>
    public float speed = 1;
    /// <summary> Whether to loop the timer </summary>
    private bool loop = false;

		/// <summary> Running time for timer </summary>
		public float timer { get; private set; }

		/// <summary> Get 0 - 1 variant of running timer </summary>
		public float percent => timer / time;
    /// <summary> Whether this timer is running or not </summary>
    public bool isRunning => timer > 0;

    /// <summary> Runs timer </summary>
    /// <param name="completeCallback"> Callback to fire upon timer completion. Fires immediately if time is zero. </param>
    /// <param name="loop"> Whether to continually loop </param>
    public void Run(Action completeCallback = null, bool loop = false) { Run(time, completeCallback, loop); }
    /// <summary> Runs timer </summary>
    /// <param name="useTime"> What time to use </param>
    /// <param name="completeCallback"> Callback to fire upon timer completion. Fires immediately if time is zero. </param>
    /// <param name="loop"> Whether to continually loop </param>
    public void Run(float useTime, Action completeCallback = null, bool loop = false) {
			time = useTime;
			timer = time;
      this.loop = loop;
      onCompleteCallback = completeCallback;
			if (time == 0)
				onCompleteCallback?.Invoke();
    }

    /// <summary> Stops the timer </summary>
    public void Stop() {
			timer = 0;
    }

    /// <summary> Updates the timer value </summary>
    /// <param name="deltaTime"> Delta time to run with </param>
    public void Update(float deltaTime = -1) {
      if (deltaTime < 0) deltaTime = Time.deltaTime;
      if (isRunning) {
				timer -= deltaTime * speed;
        if (timer <= 0) {
					timer = 0;
          onCompleteCallback?.Invoke();
          if (loop)
						timer += time;
        }
      }
    }
    
    /// <summary> Copies the values of the target </summary>
    /// <param name="target"> The target to copy </param>
    public void Copy(TimerValue target) {
			time = target.time;
			speed = target.speed;
			loop = target.loop;
			Stop();
    }
  }
}