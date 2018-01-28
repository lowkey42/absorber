using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FullscreenSprite : MonoBehaviour {

	void Start() {
		var sprite = GetComponent<SpriteRenderer> ();

		if (sprite != null) {
			var width = sprite.bounds.size.x;
			var height = sprite.bounds.size.y;

			var worldScreenHeight = Camera.main.orthographicSize * 4.0f;
			var worldScreenWidth = worldScreenHeight / Camera.main.pixelHeight * Camera.main.pixelWidth;

			if (width > 0 && height > 0 && worldScreenHeight > 0 && worldScreenWidth > 0) {
				var scale = transform.localScale;
				scale.x = worldScreenHeight / height;
				scale.y = worldScreenHeight / height;
				transform.localScale = scale;
			}
		}
	}

}
