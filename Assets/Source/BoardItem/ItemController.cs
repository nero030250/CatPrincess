using UnityEngine;
using System.Collections;

public class ItemController : MonoBehaviour {
	public static ItemController AddController (GameObject obj, ItemType type) {
		if (obj.GetComponent<ItemController> () != null)
			return obj.GetComponent<ItemController> ();

		if (type == ItemType.Person)
			return obj.AddComponent <PersonController> ();
		if (type == ItemType.Wall)
			return obj.AddComponent <WallController> ();
		if (type == ItemType.Crystal)
			return obj.AddComponent<CrystalController> ();
		if (type == ItemType.Wind)
			return obj.AddComponent<WindControl> ();
        if (type == ItemType.Dst)
            return obj.AddComponent<DstControl>();
        if (type == ItemType.Ice)
            return obj.AddComponent<IceController>();
		if (type == ItemType.Stone)
			return obj.AddComponent <StoneController> ();
		if (type == ItemType.Barrier)
			return obj.AddComponent <BarrierController> ();
        return obj.AddComponent<ItemController> ();
	}

	public ItemConfig Config { get; protected set; }
	public MoveAnimController moveController;
	protected BoardController parent;
	
	public UISprite icon;
	private GameObject lightObj;

	public virtual void Init (ItemConfig config, BoardController board) {
		moveController = GetComponent<MoveAnimController> ();
		icon = transform.Find ("ItemIcon").GetComponent<UISprite> ();
		parent = board;
		Config = config;
		gameObject.name += "_" + Config.Type.ToString ();
		if (Config.Type == ItemType.None || Config.Type == ItemType.Person) {
			icon.gameObject.SetActive(false);
			return;
		}
		icon.spriteName = string.Format ("item_{0}", Config.ID);
	}
    //有物体接近该Item时触发
	public virtual void OnClose (ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable) {
		if (OnMoveEnable != null)
			OnMoveEnable ();
	}
    //物体离开该Item时触发
    public virtual void OnRemote(ItemController controller,KeyCode direction) {

    }
	public virtual bool IsCompleted () {
		return true;
	}
}