using UnityEngine;
using System.Collections;

public class MenuUIController : MonoBehaviour {

	public static GameObject Create () {
		return UIHelper.LoadPrefab ("MenuUI");
	}

	public static MenuUIController Instance;
	public UIButton restartBtn;
	public UIButton homeBtn;
	public UIButton mask;

	void Awake () {
		Instance = this;
		EventDelegate.Add (restartBtn.onClick, () => {
			GameUIController.Instance.Init (GameUIController.Instance.Level);
			Destroy (gameObject);
		});
		EventDelegate.Add (homeBtn.onClick, () => {
			SelectLevelUIController.Create ();
			Destroy (gameObject);
		});
		EventDelegate.Add (mask.onClick, () => {
			Destroy (gameObject);});
	}
	
	void OnDestroy () {
		Instance = null;
	}
}
