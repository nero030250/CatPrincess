using UnityEngine;
using System.Collections;

public class WindControl : ItemController
{
	public override void Init (ItemConfig config, BoardController board) {
		base.Init (config, board);
		icon.gameObject.SetActive (false);
		UIHelper.LoadPrefab (string.Format ("Wind_{0}_Prefab", config.ID), transform);
	}

    /// <summary>
    ///遇到风
    /// </summary>
    /// <param name="controller">撞过来的东西</param>
    /// <param name="direction">方向</param>
    /// <param name="OnMoveEnable">移动</param>
    public override void OnClose(ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable) {
		//判断撞过来的是不是主角,则不发生任何变化
		if (controller.Config.Type != ItemType.Person) {
			return;
		}
		//获取另外一个风洞
		GameObject otherHole = parent.GetWind (this.Config);
		Vector3 nextPos = parent.NewPos (direction, otherHole.transform.localPosition);
		controller.moveController.SetPos (otherHole.transform.localPosition);
		parent.MovePerson (direction, parent.NewPos (direction, otherHole.transform.localPosition));
	}
}