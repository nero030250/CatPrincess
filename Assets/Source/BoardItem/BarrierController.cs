using UnityEngine;
using System.Collections;

public class BarrierController : ItemController {
	private bool isDestroy = false;

	public override void Init (ItemConfig config, BoardController board) {
		base.Init (config, board);
		isDestroy = false;
	}

	public override void OnClose (ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable) {
		if (isDestroy) {
			if (OnMoveEnable != null)
				OnMoveEnable ();
			return;
		}
		if (controller.Config.Type != ItemType.Stone)
			return;
		if (OnMoveEnable != null)
			OnMoveEnable ();
		isDestroy = true;
		icon.spriteName = string.Format ("item_{0}_destroy", Config.ID);
		UIHelper.LoadPrefab ("BarrierPrefab", transform);
	}
}