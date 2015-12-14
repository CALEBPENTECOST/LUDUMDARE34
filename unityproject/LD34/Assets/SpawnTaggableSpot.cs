using UnityEngine;
using System.Collections;

public class SpawnTaggableSpot : MonoBehaviour {


	public string tagSpotFolder = "TagSpots";
	public float chance = 1.0f;
	private GameObject spot = null;

	private Vector2 getBox(GameObject obj){
		var tmp = obj.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents;
		return new Vector2 (tmp.x*2.0f, tmp.y*2.0f);
	}

	// Use this for initialization
	void Start () {
		if (Random.Range (0.0f, 1.0f) < chance) {
			Vector3 buildingBox = this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents;
			Object[] found = Resources.LoadAll (tagSpotFolder, typeof(GameObject));

			//fisher-yates shuffle
			for (int i = 0; i < found.Length; i++) {
				int r = Random.Range (i, found.Length);
				Object t = found [r];
				found [r] = found [i];
				found[i] = t;
			}
			//find first tag that will fit
			foreach (Object obj in found) {
				GameObject tag = (GameObject)obj;
				Vector2 tagBox = tag.GetComponent<BoxCollider2D> ().size;
				if ((tagBox.y < buildingBox.y) && (tagBox.x < buildingBox.x)) {
					float yOffset = Random.Range (tagBox.y / 2.0f, Mathf.Clamp((buildingBox.y - tagBox.y / 2.0f),0.0f,5.0f));
					//float xOffset = Random.Range (-1.0f * (buildingBox.x - tagBox.x / 2.0f), (buildingBox.x - tagBox.x / 2.0f));

					spot = Instantiate (tag, 
						new Vector3 (this.transform.position.x, this.transform.position.y+yOffset, this.transform.position.z), Quaternion.identity) as GameObject;
					break;
				}
			}
		}
	}

}
