using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public float scrollSpeed = -0.9f;

	private float halfWidth = 40.96f;
	private bool seen = false;
	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (scrollSpeed != 0.0f) {
			this.transform.position = new Vector3 (this.transform.position.x + Time.deltaTime * scrollSpeed, this.transform.position.y, this.transform.position.z);
		}
	}

	void OnBecameVisible(){
		Instantiate(this.gameObject, new Vector3(this.transform.position.x + halfWidth, this.transform.position.y, this.transform.position.z), Quaternion.identity);
		this.seen = true;
	}

	void OnBecameInvisible() {
		if (this.seen) {
			Destroy (this);
		}
	}
}
