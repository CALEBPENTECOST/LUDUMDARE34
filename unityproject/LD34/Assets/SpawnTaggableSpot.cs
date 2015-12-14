using UnityEngine;
using System.Collections;

public class SpawnTaggableSpot : MonoBehaviour {


	public string tagSpotFolder = "TagSpots";
	public float chance = 1.0f;
	private GameObject spot = null;

	// Use this for initialization
	void Start () {
		if ((Random.Range (0.0f, 1.0f) < chance) && (spot == null)) {
			//get list of tags
			Object[] found = Resources.LoadAll (tagSpotFolder, typeof(GameObject));
			//fisher-yates shuffle on tag list
			for (int i = 0; i < found.Length; i++) {
				int r = Random.Range (i, found.Length);
				Object t = found [r];
				found [r] = found [i];
				found[i] = t;
			}

			//find first tag that will fit
			Vector3 buildingBox = this.gameObject.GetComponentInChildren<SpriteRenderer>().sprite.bounds.extents;
			float buildingHeight = buildingBox.y * 2.0f;

			foreach (Object obj in found) {
				GameObject tag = (GameObject)obj;
				Vector2 tagBox = tag.GetComponent<BoxCollider2D> ().size;
				float tagHeight = tagBox.y;
				float minAllowedTagHeight = tagHeight / 2.0f;
				if ((tagHeight < buildingHeight) && (tagBox.x < buildingBox.x)) {
					float yOffset = Random.Range (minAllowedTagHeight, Mathf.Clamp(buildingHeight - minAllowedTagHeight,minAllowedTagHeight,5.0f));
					//float xOffset = Random.Range (-1.0f * (buildingBox.x - tagBox.x / 2.0f), (buildingBox.x - tagBox.x / 2.0f));

					spot = Instantiate (tag, 
						new Vector3 (this.transform.position.x, yOffset, this.transform.position.z), Quaternion.identity) as GameObject;
					break;
				}
			}
		}
	}

}
