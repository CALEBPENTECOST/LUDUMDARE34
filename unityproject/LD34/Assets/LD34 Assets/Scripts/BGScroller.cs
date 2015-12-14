using UnityEngine;
using System.Collections;

public class BGScroller : MonoBehaviour {

	public Transform key;

	/// <summary>
	/// List of prefab objects that can spawn, or empty.
	/// </summary>
	public GameObject[] neighbors;
	/// <summary>
	/// If neighbors is empty, will load prefabs in specified folder.
	/// If null, will just clone self.
	/// </summary>
	public string neighborResourceFolder;

	private float leadDistance = 50f;

	public float widthSpacerMin = 0.0f;
	public float widthSpacerMax = 0.0f;

	private float halfWidth;

	private GameObject nextNeighbor;
	private GameObject previousNeighbor;

	void Start(){
		if ((neighbors == null) || (neighbors.Length == 0)) {
			if (string.IsNullOrEmpty (neighborResourceFolder)) {
				neighbors = new GameObject[1]{ this.gameObject };
			} else {
				Object[] found = Resources.LoadAll (neighborResourceFolder, typeof(GameObject));
				neighbors = new GameObject[found.Length];
				for (int i = 0; i < found.Length; i++) {
					neighbors [i] = found [i] as GameObject;
				}
				//Debug.Log("Found "+neighbors.Length+" neighbors for "+this.name+".");
			}
		}



		halfWidth = this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents.x;
		if (leadDistance < halfWidth) {
			leadDistance = 2 * halfWidth;
		}
	}

	private void SpawnNeighbors(){
		if ((nextNeighbor == null || previousNeighbor == null) && 
				(neighbors != null && neighbors.Length > 0)) {
			GameObject neighbor = neighbors [Random.Range (0, neighbors.Length)];
			float xOffset = halfWidth + neighbor.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents.x + Random.Range (widthSpacerMin, widthSpacerMax);
			if (nextNeighbor == null) {
				nextNeighbor = Instantiate (neighbor, new Vector3 (this.transform.position.x + xOffset, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
				nextNeighbor.GetComponentInChildren<BGScroller> ().previousNeighbor = this.gameObject;
				Debug.Log (this.name+"("+this.gameObject.transform.position.x+") spawned "+nextNeighbor.name+"("+nextNeighbor.gameObject.transform.position.x+").");
			}
			if (previousNeighbor == null) {
				previousNeighbor = Instantiate (neighbor, new Vector3 (this.transform.position.x - xOffset, this.transform.position.y, this.transform.position.z), Quaternion.identity) as GameObject;
				previousNeighbor.GetComponentInChildren<BGScroller>().nextNeighbor = this.gameObject;
				Debug.Log (this.name+"("+this.gameObject.transform.position.x+") spawned "+previousNeighbor.name+"("+previousNeighbor.gameObject.transform.position.x+").");
			}
		}
	}

	void OnBecameVisible(){
		SpawnNeighbors ();
	}

	// Update is called once per frame
	void Update () {
		if (key == null) {
			key = GameObject.FindGameObjectWithTag ("Player").GetComponent<Transform>();
		}
		if (key != null) {
			float xDist = Mathf.Abs (key.position.x - this.transform.position.x);
			if (xDist < leadDistance) {
				SpawnNeighbors ();
			}
		}
	}
}
