using UnityEngine;
using System.Collections;

public class BeginController : MonoBehaviour {
    public Texture texture;
    public TextureController textureC;
    public UIButton mask;
    //主视图
    private UITexture uit;
    // Use this for initialization
    void Start() {
        EventDelegate.Add(mask.onClick, OnMaskClick);
    }

    // Update is called once per frame
    void Update() {

    }
    //创建开始UI
    public static void Create () {
        GameObject obj = UIHelper.LoadPrefab("BeGinUI");
        obj.GetComponent<BeginController>().Init();
    }
    //
    public  void Init () {
        uit = textureC.GetComponent<UITexture>();
        uit.mainTexture = texture;
    }
    public void OnMaskClick () {
        Destroy(this.gameObject);
        //跳转游戏主页
        DramaController.Create();
        
    }
}
