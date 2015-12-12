using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public Transform key;

	private float halfWidth;
	public float leadDistance = 500.0f;
	public bool spawnedNext = false;
	public bool spawnedPrevious = false;

	void Start(){
		//todo: determine halfWidth from sprite component
		halfWidth = GetComponent<SpriteRenderer>().sprite.bounds.extents.x;
		if (leadDistance < halfWidth) {
			leadDistance = 2 * halfWidth;
		}
	}

	private void SpawnNeighbors(){
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

	void OnBecameVisible(){
		SpawnNeighbors ();
	}

	// Update is called once per frame
	void Update () {
		if ((!spawnedNext || !spawnedPrevious)) {
			float xDist = Mathf.Abs(key.position.x - this.transform.position.x);
			if (xDist < leadDistance) {
				SpawnNeighbors ();
			}
		}
	}
}
