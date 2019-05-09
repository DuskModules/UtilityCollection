using System;

namespace DuskModules {

	/// <summary> Callback waiting for all things to complete before firing. </summary>
	public class WaitingCallback {

		public Action callback;

		private int waitCount;
		private bool waitSet;
		private int hitCount;
		private bool done;

		/// <summary> Creates the callback. </summary>
		public WaitingCallback(Action callback) {
			this.callback = callback;
			done = false;
		}

		/// <summary> Sets the count it waits for. Returns whether completed. </summary>
		public void SetWait(int wait) {
			waitCount = wait;
			waitSet = true;
			CheckHit();
		}

		/// <summary> Fired when something completes. Returns whether completed. </summary>
		public void CallbackHit() {
			hitCount++;
			CheckHit();
		}

		private void CheckHit() {
			if (!done && waitSet && hitCount >= waitCount) {
				callback();
				done = true;
			}
		}
	}
}