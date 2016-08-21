using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AnimController : MonoBehaviour {
	public const float FRAME = 8f;

	public List<AnimData> data = new List<AnimData> ();
	public UISprite sprite;

	private int curAnim = 0;
	private string itemName;

	[HideInInspector]public bool IsIdle = true;

	void Start () {
		itemName = sprite.spriteName.Split ('_') [0];
		StartCoroutine (_Anim ());
	}

	public void PlayAnim (string name) {
		int index = data.FindIndex ((a) => {
			return a.aniName == name;});
		if (index >= 0)
			curAnim = index;
	}

	public IEnumerator _Anim () {
		float t = 0, curIndex = 0;
		int lastAnimIndex = curAnim;
		while (true) {
			if (IsIdle && data[curAnim].group != 0) {
				RandomIdleAnim ();
			}
			if (lastAnimIndex != curAnim) {
				lastAnimIndex = curAnim;
				t = 0;
				curIndex = 0;
			}
			t += Time.deltaTime;
			if (t >= 1f / FRAME) {
				curIndex ++;
				if (curIndex == data[curAnim].frameCount) {
					t = 0;
					curIndex = 0;
					if (data[curAnim].group == 0)
						RandomIdleAnim ();
					continue;
				}
				sprite.spriteName = string.Format ("{0}_{1}_{2}", itemName, data[lastAnimIndex].aniName, curIndex);
				t = 0;
			}
			yield return null;
		}
	}

	public void RandomIdleAnim () {
		List<AnimData> list = data.FindAll ((a) => {
			return a.group == 0;});
		if (list.Count == 0)
			return;
		curAnim = data.IndexOf (list[Random.Range (0, list.Count)]);
	}
}