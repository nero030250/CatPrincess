using UnityEngine;
using System.Collections;

public class InputM : MonoBehaviour {

	public static InputM Instance { get; private set; }

	public SystemDelegate.KeyCodeDelegate OnKeyDown;
	private Vector3 lastClickPos;
	public float MIN_MOVE = 10f;

	void Awake () {
		Instance = this;
	}
	
	void OnDestroy () {
		Instance = null;
	}

	void Update () {
		if (Input.GetMouseButtonDown (0)) {
			lastClickPos = Input.mousePosition;
		} else if (Input.GetMouseButtonUp (0)) {
			KeyCode kc = SelectKey (lastClickPos, Input.mousePosition);
			if (kc != KeyCode.None && OnKeyDown != null)
				OnKeyDown (kc);
		}
		if (Input.GetKeyDown (KeyCode.UpArrow))
			OnKeyDown (KeyCode.UpArrow);
		if (Input.GetKeyDown (KeyCode.DownArrow))
			OnKeyDown (KeyCode.DownArrow);
		if (Input.GetKeyDown (KeyCode.LeftArrow))
			OnKeyDown (KeyCode.LeftArrow);
		if (Input.GetKeyDown (KeyCode.RightArrow))
			OnKeyDown (KeyCode.RightArrow);
	}

	private KeyCode SelectKey (Vector3 downPos, Vector3 upPos) {
		float xOffset = Mathf.Abs (upPos.x - downPos.x);
		float yOffset = Mathf.Abs (upPos.y - downPos.y);
		if (xOffset > yOffset) {
			if (upPos.x - downPos.x > MIN_MOVE)
				return KeyCode.RightArrow;
			if (downPos.x - upPos.x > MIN_MOVE)
				return KeyCode.LeftArrow;
		} else {
			if (upPos.y - downPos.y > MIN_MOVE)
				return KeyCode.UpArrow;
			if (downPos.y - upPos.y > MIN_MOVE)
				return KeyCode.DownArrow;
		}
		return KeyCode.None;
	}
}