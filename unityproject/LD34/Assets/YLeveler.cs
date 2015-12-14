using UnityEngine;
using System.Collections;

public class YLeveler : MonoBehaviour {


	public float YOffset = 0.0f;

	// Use this for initialization
	void Start () {
		SpriteRenderer r = this.gameObject.GetComponentInChildren<SpriteRenderer> ();
		if (r != null) {
			Vector3 spriteBox = r.sprite.bounds.extents;
			Vector3 oldPos = this.transform.position;
			//float scale = this.gameObject.transform.localScale.y;
			this.transform.position = new Vector3 (oldPos.x, YOffset + spriteBox.y, oldPos.z);
		}
	}
}
