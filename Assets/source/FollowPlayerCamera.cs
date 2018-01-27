using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowPlayerCamera : MonoBehaviour {

	[SerializeField] protected string followTag = "Player1";
	public float damping = 4;
	public float lookAheadFactor = 4;
	public float lookAheadReturnSpeed = 1f;
	public float lookAheadMoveThreshold = 0.1f;
	public float minDeltaX = 4;
	public float minDeltaY = 6;

	private GameObject target;
	private Vector3 m_LastTargetPosition;
	private Vector3 m_CurrentVelocity;
	private Vector3 m_LookAheadPos;

	// Update is called once per frame
	void LateUpdate () {
		if (target != null && target.activeInHierarchy) {
			if (target.GetComponent<AlwaysCenterCamera> () != null) {
				transform.position = target.transform.position + Vector3.forward * -4;
				m_LastTargetPosition = target.transform.position;
				return;
			}

			// only update lookahead pos if accelerating or changed direction
			float xMoveDelta = (target.transform.position - m_LastTargetPosition).x;

			bool updateLookAheadTarget = Mathf.Abs(xMoveDelta) > lookAheadMoveThreshold;

			if (updateLookAheadTarget)
			{
				m_LookAheadPos = lookAheadFactor*Vector3.right*Mathf.Sign(xMoveDelta);
			}
			else
			{
				m_LookAheadPos = Vector3.MoveTowards(m_LookAheadPos, Vector3.zero, Time.deltaTime*lookAheadReturnSpeed);
			}

			Vector3 aheadTargetPos = target.transform.position + m_LookAheadPos + Vector3.forward*-4;
			Vector3 newPos = Vector3.SmoothDamp(transform.position, aheadTargetPos, ref m_CurrentVelocity, damping);

			var diff = aheadTargetPos - transform.position;

			if (Mathf.Abs (diff.x) < minDeltaX)
				newPos.x = transform.position.x;

			if (Mathf.Abs (diff.y) < minDeltaY)
				newPos.y = transform.position.y;

			transform.position = newPos;

			m_LastTargetPosition = target.transform.position;


		} else {
			target = GameObject.FindGameObjectWithTag (followTag);
			if (target != null && target.activeInHierarchy) {
				transform.position = target.transform.position + Vector3.forward * -4;
				m_LastTargetPosition = target.transform.position;
			}
		}
	}
}
