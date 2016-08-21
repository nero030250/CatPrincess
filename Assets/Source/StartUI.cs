using UnityEngine;
using System.Collections;

public class StartUI : MonoBehaviour {
	public static StartUI Instance;
	public UIButton closeBtn;

	void Awake () {
		Instance = this;
		EventDelegate.Add (closeBtn.onClick, () => {
			Destroy (gameObject);
			GameObject obj = GameObject.Instantiate (Resources.Load ("Prefab/GameUI")) as GameObject;
			obj.transform.parent = BoardM.Instance.UIParent;
			obj.transform.localScale = Vector3.one;
			obj.transform.localPosition = Vector3.zero;
		});
	}
	
	void OnDestroy () {
		Instance = null;
	}
}
