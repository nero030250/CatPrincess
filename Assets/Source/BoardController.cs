using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class BoardController : MonoBehaviour {

	public List<Texture> bgList;

	public UITexture BG;
	public UIGridEx grid;
	public Transform itemBgParent;
	public PersonController person;
	public bool IsMirrow;
	public SystemDelegate.KeyCodeDelegate OnMoveEnable;
    private List<ItemConfig> configList = new List<ItemConfig> ();

	private Vector2 topLeft;
	private Vector2 bottomRight;   

	void Awake () {
		grid.OnItemInitilaze = OnItemInitilaze;
		grid.SetGrid (BoardM.ITEM_SIZE, BoardM.ITEM_SIZE, BoardM.WIDTH);
		topLeft = Vector2.zero;
		bottomRight = new Vector2 ((BoardM.WIDTH - 1) * BoardM.ITEM_SIZE, -(BoardM.HEIGHT - 1) * BoardM.ITEM_SIZE);
	}

	public void Init (int [,] data) {
		BG.mainTexture = bgList [GameUIController.Instance.Level];
		BG.MakePixelPerfect ();
		configList.Clear ();
		int startIndex = 0;
		for (int x = 0; x < data.GetLength (0); x ++) {
			for (int y = 0; y < data.GetLength (1); y ++) {
				ItemConfig config = new ItemConfig (data[x, y]);
				configList.Add (config);
				if (config.Type == ItemType.Person)
					startIndex = configList.Count - 1;
			}
		}
		grid.DestroyAll ();
		grid.Resize (BoardM.WIDTH * BoardM.HEIGHT);
		person.Init (new ItemConfig (1), this);
		person.moveController.SetPos (Item (startIndex).gameObject.transform.localPosition);
	}

	private void OnItemInitilaze (GameObject obj, int index) {
		ItemController.AddController (obj, configList [index].Type).Init (configList[index], this);
	}

	public bool IsCompleted () {
		foreach (GameObject obj in grid.ItemList) {
			if (!obj.GetComponent<ItemController> ().IsCompleted ())
				return false;
		}
		return true;
	}

	private Vector3 upMove = Vector3.up * BoardM.ITEM_SIZE;
	private Vector3 downMove = - Vector3.up * BoardM.ITEM_SIZE;
	private Vector3 leftMove = - Vector3.right * BoardM.ITEM_SIZE;
	private Vector3 rightMove = Vector3.right * BoardM.ITEM_SIZE;

	public void OnKeyDown (KeyCode key) {
		if (person.isMoving || GameUIController.Instance.isOver)
			return;
		if (IsMirrow) {
			if (key == KeyCode.LeftArrow)
				key = KeyCode.RightArrow;
			else if (key == KeyCode.RightArrow)
				key = KeyCode.LeftArrow;
		}

		Vector3 dst = NewPos (key, person.transform.localPosition);
		if (OutOfRange (dst))
			return;
		ItemController curPosItem = GetItemByPos (person.transform.localPosition);
		if (curPosItem != null)
			curPosItem.OnRemote (person, key);

		ItemController item = GetItemByPos (dst);
		if (item != null) {
			item.OnClose (person, key, () => {
				MovePerson (key, dst, item.Config.Type);
			});
		} else {
			MovePerson (key, dst);
		}
	}

	public void MovePerson (KeyCode key, Vector3 dst, ItemType dstType = ItemType.None) {
		person.moveController.Move (key, dst, () => {
			if (dstType == ItemType.Dst)
				GameUIController.Instance.IsCompleted ();});
		if (OnMoveEnable != null)
			OnMoveEnable (key);
	}

	public Vector3 NewPos (KeyCode key, Vector3 oldPos) {		
		switch (key) {
		case KeyCode.UpArrow:
			return oldPos + upMove;
		case KeyCode.DownArrow:
			return oldPos +  downMove;
		case KeyCode.LeftArrow:
			return oldPos +  leftMove;
		case KeyCode.RightArrow:
			return oldPos +  rightMove;
		}
		return oldPos;
	}

	public bool OutOfRange (Vector3 pos) {
		int x = Mathf.RoundToInt (pos.x), y = Mathf.RoundToInt (pos.y);
		return x < topLeft.x || x > bottomRight.x || y < bottomRight.y || y > topLeft.y;
	}

	public ItemController GetItemByPos (Vector3 pos) {
		GameObject item =  grid.GetItem (pos, (obj) => {
			return obj.GetComponent<ItemController> ().Config.Type != ItemType.None;
		});
		if (item == null)
			return null;
		return item.GetComponent<ItemController> ();
	}

	private ItemController Item (int index) {
		return grid.GetItem (index).GetComponent<ItemController> ();
	}

    /// <summary>
    /// 获取所有对应的风洞
    /// </summary>
    /// <param name="item">风洞</param>
    /// <returns></returns>
    public GameObject GetWind(ItemConfig item)
    {
        for (int index = 0; index < configList.Count; index++)
        {
            if (configList[index].Type == item.Type && configList[index] != item)
            {
                if (grid.ItemList.Count > index)
                {
                    return grid.ItemList[index];
                }
            }
        }
        return null;
    }
}