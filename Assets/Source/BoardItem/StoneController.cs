using UnityEngine;
using System.Collections;

public class StoneController : ItemController {
	private bool isDestroy = false;

	public override void Init (ItemConfig config, BoardController board) {
		base.Init (config, board);
		isDestroy = false;
		icon.gameObject.SetActive (true);
	}

	public override void OnClose (ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable) {
		if (isDestroy) {
			if (OnMoveEnable != null)
				OnMoveEnable ();
			return;
		}
		if (controller.Config.Type != ItemType.Person)
			return;
		Vector3 pos = parent.NewPos (direction, transform.localPosition);
		if (parent.OutOfRange (pos))
			return;
		ItemController close = parent.GetItemByPos (pos);
		if (close == null) {
			moveController.Move (direction, pos);
			if (OnMoveEnable != null)
				OnMoveEnable ();
		} else {
			close.OnClose (this, direction, () => {
				moveController.Move (direction, pos, () => {
					if (close.Config.Type == ItemType.Barrier) {
						isDestroy = true;
						icon.gameObject.SetActive (false);
					}
				});
				if (OnMoveEnable != null)
					OnMoveEnable ();
			});
		}
	}
}