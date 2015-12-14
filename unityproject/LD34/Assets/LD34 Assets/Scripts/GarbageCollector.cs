using UnityEngine;
using System.Collections;

public class GarbageCollector : MonoBehaviour {

	public Transform key;
	private float leadDistance = 60.0f;

	// Update is called once per frame
	void Update () {
		if (key == null) {
			key = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		}
		float xDist = Mathf.Abs(key.position.x - this.gameObject.transform.position.x);
		if (xDist > leadDistance) {
			Debug.Log ("Destroying " + this.name + "("+this.gameObject.transform.position.x+").");
			Destroy (this.gameObject);
		}
	}
}
