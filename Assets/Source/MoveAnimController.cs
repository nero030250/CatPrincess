using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using Holoville.HOTween;

public class MoveAnimController : MonoBehaviour {
	public UISprite sprite;
	public const float MOVE_DURATION = 0.3f;
	[HideInInspector] public bool isPlaying = false;
	public AnimController animController;

	public void Move (KeyCode key, Vector3 dst, SystemDelegate.VoidDelegate onMoveComplete = null) {
		if (isPlaying)
			return;
		isPlaying = true;
		if (key == KeyCode.RightArrow)
			sprite.flip = UIBasicSprite.Flip.Nothing;
		else if (key == KeyCode.LeftArrow)
			sprite.flip = UIBasicSprite.Flip.Horizontally;
		if (animController != null) {
			animController.PlayAnim ("R");
			animController.IsIdle = false;
		}
		HOTween.To (transform, MOVE_DURATION, new TweenParms ().Prop ("localPosition", dst).Ease (EaseType.Linear).OnComplete (() => {
			isPlaying = false;
			if (animController != null)
				animController.IsIdle = true;
			if (onMoveComplete != null)
				onMoveComplete ();
		}));
    }

	public void SetPos (Vector3 pos) {
		transform.localPosition = pos;
	}
}