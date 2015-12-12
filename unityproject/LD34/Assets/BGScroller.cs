using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public static float scrollSpeed = -0.9f;

	public float halfWidth = 40.96f;

	void Start(){
		//todo: determine halfWidth from sprite component
		if (scrollSpeed > 0) {
			halfWidth *= -1.0f;
		}
	}

	// Update is called once per frame
	void Update () {
		if (scrollSpeed != 0.0f) {
			this.transform.position = new Vector3 (this.transform.position.x + Time.deltaTime * scrollSpeed, this.transform.position.y, this.transform.position.z);
		}
	}

	void OnBecameVisible(){
		Instantiate(this.gameObject, new Vector3(this.transform.position.x + halfWidth, this.transform.position.y, this.transform.position.z), Quaternion.identity);
	}
}
