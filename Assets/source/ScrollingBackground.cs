using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollingBackground : MonoBehaviour {

	private FollowPlayerCamera followComp;

	//speed of scrolling
	public float speed;

	private float initPos;

	void Start () {
		initPos = transform.localPosition.x;
		//Create a clone for filling rest of the screen
		GameObject objectCopy = GameObject.Instantiate (this.gameObject);
		//Destroy ScrollBackground component in clone
		Destroy(objectCopy.GetComponent<ScrollingBackground> ());
		Destroy(objectCopy.GetComponent<FullscreenSprite> ());
		//Set clone parent and position
		objectCopy.transform.SetParent (this.transform);
		objectCopy.transform.localPosition = new Vector3 (getWidth(), 0, 0);
		objectCopy.transform.localScale = new Vector3 (1, 1, 1);

		followComp = GetComponentInParent<FollowPlayerCamera> ();
		if (followComp != null) {
			var layer = followComp.getTargetTag () == "Player1" ? LayerMask.NameToLayer("BG_cam1") : LayerMask.NameToLayer("BG_cam2");

			gameObject.layer = layer;
			objectCopy.layer = layer;
			GetComponentInParent<Camera> ().cullingMask |= 1<<layer;
		}
	}

	void FixedUpdate () {
		Rigidbody2D target = null;

		if (followComp != null) {
			var targetGo = followComp.getTarget ();
			if (targetGo != null) {
				target = targetGo.GetComponent<Rigidbody2D> ();
			}
		}

		if (target == null)
			return;

		//get target velocity
		//if you wish to replace target with a non-rigidbody object, this is the place
		float targetVelocity = target.velocity.x;
		//translate sprite according to target velocity
		this.transform.Translate (new Vector3 (-speed * targetVelocity, 0, 0) * Time.deltaTime);
		//set sprite is moving out of screen shift it to put clone in its place
		float width = getWidth ();
		if (targetVelocity > 0) {
			//shift right if player is moving right
			if (initPos - this.transform.localPosition.x > width) {
				this.transform.Translate (new Vector3 (width*transform.localScale.x, 0, 0)); 
			}
		} else {
			//shift left if player moving left
			if (initPos - this.transform.localPosition.x < 0) {
				this.transform.Translate (new Vector3 (-width*transform.localScale.x, 0, 0)); 
			}
		}
	}

	float getWidth() {
		//Get sprite width
		return this.GetComponent<SpriteRenderer> ().bounds.size.x/transform.localScale.x;
	}

}
