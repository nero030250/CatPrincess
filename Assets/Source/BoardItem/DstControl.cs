using UnityEngine;
using System.Collections;

public class DstControl : ItemController {
	private bool isOk = false;
	private GameObject dstPrefab;
	public override void Init (ItemConfig config, BoardController board) {
		base.Init (config, board);
		isOk = false;
		icon.gameObject.SetActive (false);
		if (dstPrefab == null) {
			dstPrefab = UIHelper.LoadPrefab ("DstPrefab", transform);
		}
		ChangeDstPrefab ();
	}

    public override void OnClose (ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable) {
        //判断撞过来的是不是主角,则不发生任何变化
        if (controller.Config.Type != ItemType.Person) {
            return;
        }
		isOk = true;
        if (OnMoveEnable != null)
			OnMoveEnable ();
		ChangeDstPrefab ();
    }

    /// <summary>
    /// 当有物体远离终点
    /// </summary>
    /// <param name="controller"></param>
    /// <param name="direction"></param>
    public override void OnRemote (ItemController controller, KeyCode direction) {
        //判断撞过来的是不是主角,则不发生任何变化
        if (controller.Config.Type != ItemType.Person) {
            return;
        }
        //是主角，则达到要求，并且播放关闭光泽动画
		isOk = false;
		ChangeDstPrefab ();
    }

    //是否达到要求
    public override bool IsCompleted() {
        return isOk;
    }

	private void ChangeDstPrefab () {
		if (!isOk) {
			dstPrefab.transform.Find ("Idle").gameObject.SetActive (true);
			dstPrefab.transform.Find ("Completed").gameObject.SetActive (false);
			dstPrefab.transform.Find ("Fail").gameObject.SetActive (false);
		} else {
			if (GameUIController.Instance.IsCompleted ()) {
				dstPrefab.transform.Find ("Idle").gameObject.SetActive (false);
				dstPrefab.transform.Find ("Completed").gameObject.SetActive (true);
				dstPrefab.transform.Find ("Fail").gameObject.SetActive (false);
			} else {
				dstPrefab.transform.Find ("Idle").gameObject.SetActive (false);
				dstPrefab.transform.Find ("Completed").gameObject.SetActive (false);
				dstPrefab.transform.Find ("Fail").gameObject.SetActive (true);
			}
		}
	}
}
