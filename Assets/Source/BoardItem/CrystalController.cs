using UnityEngine;
using System.Collections;

public class CrystalController : ItemController {
    //是否达到要求
    private bool isOk = false;
	private GameObject crystalObj;
	public override void Init (ItemConfig config, BoardController board) {
		base.Init (config, board);
		isOk = false;
		icon.gameObject.SetActive (false);
		if (crystalObj == null) {
			crystalObj = UIHelper.LoadPrefab ("CrystalPrefab", transform);
		}
		crystalObj.transform.Find ("EndImage").gameObject.SetActive (false);
	}
   
    /// <summary>
    ///遇到水晶 
    /// </summary>
    /// <param name="controller">撞过来的东西</param>
    /// <param name="direction">方向</param>
    /// <param name="OnMoveEnable">移动</param>
    public override void OnClose(ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable)
    {
        //判断撞过来的是不是主角,则不发生任何变化
        if (controller.Config.Type != ItemType.Person) {
            return;
        }
        //是主角，则达到要求，并且播放发光动画
        isOk = true;
		if (OnMoveEnable != null)
			OnMoveEnable ();
    }

	public override void OnRemote (ItemController controller, KeyCode direction) {
		crystalObj.transform.Find ("EndImage").gameObject.SetActive (true);
	} 

    //是否达到要求
    public override bool IsCompleted() {
        return isOk;
    }
}
