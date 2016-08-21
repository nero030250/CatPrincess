using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class GameUIController : MonoBehaviour {
	public static GameUIController Instance { get; private set; }

	public int Level;
	public UILabel tip;
	public BoardController leftBoard;
	public BoardController rightBoard;

	public UIButton menuBtn;

	public bool isOver = false;

	public static void Create (int level) {
		if (Instance == null) {
			GameObject obj = UIHelper.LoadPrefab ("GameUI");
			Instance = obj.GetComponent<GameUIController> ();
		}
		Instance.Init (level);
	}

	void Awake () {
		Instance = this;
	}

	void Start () {
		InputM.Instance.OnKeyDown += rightBoard.OnKeyDown;
		rightBoard.OnMoveEnable += leftBoard.OnKeyDown;

		EventDelegate.Add (menuBtn.onClick, () => {
			MenuUIController.Create ();
		});
	}

	void OnDestroy () {
		Instance = null;
	}

	public void Init (int level) {
		Level = level;
		Board board = BoardM.Instance.GetBoardByLevel (level);
		ShowTips (board.tip, 0);
		leftBoard.Init (board.leftCsv);
		rightBoard.Init (board.rightCsv);
		if (completedObj != null)
			Destroy (completedObj);
		isOver = false;
	}

	private void ShowTips (List<string> strList, int index) {
		if (index >= strList.Count)
			return;
		tip.text = strList [index];
		TweenAlpha t = tip.GetComponent<TweenAlpha> ();
		EventDelegate.Add (t.onFinished, () => {
			EventDelegate.Add (t.onFinished, () => {
				ShowTips (strList, index + 1);
			}, true);
			t.PlayReverse ();
		}, true);
		t.PlayForward ();
	}

	public bool IsCompleted () {
//    		if (leftBoard.person.isMoving || rightBoard.person.isMoving)
//			return false;  	
		bool isCompleted = rightBoard.IsCompleted () && leftBoard.IsCompleted ();
		if (isCompleted) {
			ShowCompleted ();
			BoardM.Instance.Completed (Level);
		}
		return isCompleted;
	}	

	private GameObject completedObj;
	private void ShowCompleted () {
		if (isOver)
			return;
		isOver = true;
		completedObj = UIHelper.LoadPrefab (string.Format ("Completed_{0}", Level), transform);
		StartCoroutine (_DelayOpen ());
	}

	private IEnumerator _DelayOpen () {
		yield return new WaitForSeconds (3f);
		if (Level > 0)
			SelectLevelUIController.Create ();
		else 
			Init (1);
	}
}