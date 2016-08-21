using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class DramaController : MonoBehaviour {
    public Texture[] imgArray;
    public UITexture textureC;
    private int nowIndex=0;

	public UIButton mask;

    //创建剧情UI
    public static void Create() {
        GameObject obj = UIHelper.LoadPrefab("DramaUI");
    }
    void Start() {
		EventDelegate.Add (mask.onClick, OnMaskClick);
    }

	public void OnMaskClick () {
		nowIndex ++;
		if (nowIndex < imgArray.Length) 
			textureC.mainTexture = imgArray [nowIndex];
		else {
			Destroy(this.gameObject);
			GameUIController.Create(0);
		}
    }
}