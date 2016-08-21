using UnityEngine;
using System.Collections;

public class UIHelper : MonoBehaviour {
	public static GameObject LoadPrefab (string name) {
		return LoadPrefab (name, BoardM.Instance.UIParent);
	}

	public static GameObject LoadPrefab (string name, Transform parent) {
		Object ob = Resources.Load ("Prefab/" + name);
		if (ob == null)
			return null;
		GameObject obj = GameObject.Instantiate (ob) as GameObject;
		obj.transform.parent = parent;
		obj.transform.localScale = Vector3.one;
		obj.transform.localPosition = Vector3.zero;
		return obj;
	}
}
