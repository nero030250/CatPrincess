using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class SelectLevelUIController : MonoBehaviour {
	public static SelectLevelUIController Instance { get; private set; }

	public static void Create () {
		UIHelper.LoadPrefab ("LevelSelectUI");
	}

	void Awake () {
		Instance = this;
	}

	void Start () {
		for (int index = 1; index <= BoardM.Instance.MaxLevel; index ++) {
			GameObject levelBtn = transform.Find (string.Format ("{0}", index)).gameObject;
			levelBtn.gameObject.SetActive (true);
			if (index == BoardM.Instance.MaxLevel)
				levelBtn.transform.Find ("Normal").gameObject.SetActive (true);
			else 
				levelBtn.transform.Find ("Light").gameObject.SetActive (true);
			EventDelegate.Add (levelBtn.GetComponent<UIButton> ().onClick, () => {
				GameUIController.Create (int.Parse (levelBtn.name));
				Destroy (gameObject);
			});
		}
	}

	void OnDestroy () {
		Instance = null;
	}
}