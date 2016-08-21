using UnityEngine;
using System.Collections;

public class PersonController : ItemController {
	public override void Init (ItemConfig config, BoardController board) {
		Config = config;
		parent = board;
	}

	public bool isMoving {
		get {
			return moveController.isPlaying;
		}
	}
}