using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour {

	private bool seen = false;
	private bool invisible = false;
	private float timeInvisible = 0.0f;
	public static float timeLimit = 60.0f; 

	// Update is called once per frame
	void Update () {
		if (this.invisible && this.seen) {
			this.timeInvisible += Time.deltaTime;
			if (this.timeInvisible > timeLimit) {
				Debug.Log ("Destroying "+this.name+".");
				Destroy (this.gameObject);
			}
		}
	}

	void OnBecameVisible(){
		this.seen = true;
		this.timeInvisible = 0.0f;
		this.invisible = false;
	}

	void OnBecameInvisible() {
		this.invisible = true;
	}
}
