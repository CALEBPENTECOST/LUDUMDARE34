using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public float scrollSpeed = 0.0f;

	public float halfWidth = 40.96f;

	public bool spawnedNext = false;
	public bool spawnedPrevious = false;

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
		bool curNext = spawnedNext;
		bool curPrev = spawnedPrevious;
		if (!spawnedNext) {
			spawnedNext = false;
			spawnedPrevious = true;
			Instantiate (this.gameObject, new Vector3 (this.transform.position.x + halfWidth, this.transform.position.y, this.transform.position.z), Quaternion.identity);
			spawnedNext = true;
			spawnedPrevious = curPrev;
		}
		if (!spawnedPrevious) {
			spawnedPrevious = false;
			spawnedNext = true;
			Instantiate (this.gameObject, new Vector3 (this.transform.position.x - halfWidth, this.transform.position.y, this.transform.position.z), Quaternion.identity);
			spawnedPrevious = true;
			spawnedNext = curNext;
		}
	}
}
