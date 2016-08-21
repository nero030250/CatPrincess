using UnityEngine;
using System.Collections;

public class IceController : ItemController
{
    /// <summary>
    ///遇到水晶 
    /// </summary>
    /// <param name="controller">撞过来的东西</param>
    /// <param name="direction">方向</param>
    /// <param name="OnMoveEnable">移动</param>
    public override void OnClose(ItemController controller, KeyCode direction, SystemDelegate.VoidDelegate OnMoveEnable)
    {
        if (controller.Config.Type != ItemType.Person)
            return;
        //方向，冰块的下一个坐标
        Vector3 nextPos = parent.NewPos(direction,gameObject.transform.position);
        //移动
        if (OnMoveEnable != null)
            OnMoveEnable();
        //再移动
        controller.moveController.Move(direction,nextPos);
    }
}