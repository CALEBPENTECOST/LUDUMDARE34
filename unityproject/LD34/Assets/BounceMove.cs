using UnityEngine;
using System.Collections;

public class BounceMove : MonoBehaviour {

	public Vector2 path = Vector2.zero;
	//public float speed = 1.9f;
	//public float damping = 0.1f;
	private Vector3 startPos;
	private Vector3 endPos;
	private Transform t;
	//private float progress = 0.0f;
	private Vector3 m_CurrentVelocity;
	// Use this for initialization
	void Start () {
		t = this.gameObject.transform;
		startPos = t.position;
		endPos = t.position + new Vector3 (path.x, path.y, startPos.z);
	}
	
	// Update is called once per frame
	void Update () {
		float progress = 0.5f + Mathf.Sin(Time.fixedTime)/2.0f;

		Vector3 curPos = this.transform.position;
		this.transform.position = Vector3.Lerp (startPos, endPos, progress);
		//this.transform.position = Vector3.SmoothDamp(curPos, targetPos, ref m_CurrentVelocity, damping);

	}

}
