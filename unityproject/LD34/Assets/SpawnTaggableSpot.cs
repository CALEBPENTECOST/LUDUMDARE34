using UnityEngine;
using System.Collections;

public class SpawnTaggableSpot : MonoBehaviour {


	public string tagSpotFolder = "TagSpots";
	public float chance = 0.5f;
	private GameObject spot = null;

	// Use this for initialization
	void Start () {
		if (Random.Range (0.0f, 1.0f) < chance) {
			float halfHeight = this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents.y;
			float yOffset = Random.Range (0.0f, Mathf.Clamp(halfHeight,0.0f,5.0f));
			Object[] found = Resources.LoadAll (tagSpotFolder, typeof(GameObject));
			int spotIndex = Random.Range (0, found.Length);
			spot = Instantiate ((GameObject)found[spotIndex], 
					new Vector3 (this.transform.position.x, this.transform.position.y+yOffset, this.transform.position.z), Quaternion.identity) as GameObject;
		}
	}

}
