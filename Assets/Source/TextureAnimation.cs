using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TextureAnimation : MonoBehaviour {
	public Texture[] images;
	public UITexture show;
	public bool loop = false;
	public float frame = 8;

	void Start () {
		StartCoroutine (_PlayAnim ());
	}

	private IEnumerator _PlayAnim () {
		float time = 0;
		int index = 0;
		while (true) {
			if (time > (1f / frame)) {
				time = 0;
				index ++;
				if (index == images.Length) {
					if (!loop)
						yield break;
					index = 0;
				}
			}
			show.mainTexture = images [index];
			time += Time.deltaTime;
			yield return null;
		}
	}
}