using UnityEngine;
using System.Collections;

public class ld34CameraFollow : MonoBehaviour {

	public Transform target;
	public float damping = 0.2f;
	//public float lookAheadFactor = 100;
	//public float lookAheadReturnSpeed = 0.5f;
	//public float lookAheadMoveThreshold = 0f;

	public float staticXOffset = 10.0f;
	public float staticYOffset = 0.0f;

	private float m_OffsetZ;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	//private Vector3 m_LookAheadPos;

	// Use this for initialization
	private void Start()
	{
		m_LastTargetPosition = target.position;
		m_OffsetZ = (transform.position - target.position).z;
		transform.parent = null;
	}


	// Update is called once per frame
	private void Update()
	{
		// only update lookahead pos if accelerating or changed direction
		//float xMoveDelta = (target.position - m_LastTargetPosition).x;

		/*bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

		if (updateLookAheadTarget)
		{
			m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
		}
		else
		{
			m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
		}*/

		//Vector3 aheadTargetPos = target.position + Vector3.right*staticXOffset + Vector3.forward*m_OffsetZ;
		Vector3 aheadTargetPosY = new Vector3(0, target.position.y + staticYOffset, 0);
		Vector3 currentPosY = new Vector3(0, transform.position.y, 0);
		Vector3 midPosY = Vector3.SmoothDamp(currentPosY, aheadTargetPosY, ref m_CurrentVelocity, damping);


		transform.position = midPosY +
			Vector3.right * (target.position.x + staticXOffset) + 
			(Vector3.forward*m_OffsetZ);

		m_LastTargetPosition = target.position;
	}
}
