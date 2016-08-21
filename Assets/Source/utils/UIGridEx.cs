using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[RequireComponent (typeof(UIGrid))]
public class UIGridEx : MonoBehaviour {
	private UIGrid grid;
	public GameObject itemPrefab;
	public List<GameObject> ItemList = new List<GameObject> ();
	public SystemDelegate.OnItemInitilaze OnItemInitilaze;

	public void Awake () {
		grid = gameObject.GetComponent <UIGrid> ();
	}

	public void SetGrid (int width, int height, int limit) {
		grid = gameObject.GetComponent <UIGrid> ();
		grid.cellWidth = width;
		grid.cellHeight = height;
		grid.maxPerLine = limit;
		grid.repositionNow = true;
	}

	public void Resize (int count) {
		for (int index = 0; index < count; index ++) {
			if (ItemList.Count <= index) {
				GameObject obj = GameObject.Instantiate (itemPrefab);
				obj.transform.parent = transform;
				obj.transform.localScale = Vector3.one;
				obj.name = index.ToString ("D2");
				ItemList.Add (obj);
			}
			if (grid.arrangement == UIGrid.Arrangement.Horizontal)
				ItemList [index].transform.localPosition = new Vector3 (index % grid.maxPerLine * grid.cellWidth, - (index / grid.maxPerLine * grid.cellHeight), 0);
			else 
				ItemList [index].transform.localPosition = new Vector3 (index / grid.maxPerLine * grid.cellWidth, - index % grid.maxPerLine * grid.cellHeight, 0);
			ItemList [index].SetActive (true);
			OnItemInitilaze (ItemList [index], index);
		}
		for (int index = count; index < ItemList.Count; index ++) {
			ItemList [index].SetActive (false);
		}
	}

	public GameObject GetItem (Vector3 pos, SystemDelegate.BoolIsTarget IsTarget) {
		foreach (GameObject item in ItemList) {
			if (item.transform.localPosition.Equals (pos) && IsTarget (item))
				return item;
		}
		return null;
	}

	public GameObject GetItem (int index) {
		if (index < ItemList.Count)
			return ItemList[index];
		return null;
	}

	public void DestroyAll () {
		for (int index = 0; index < ItemList.Count; index ++) {
			Destroy (ItemList[index]);
		}
		ItemList.Clear ();
	}
}
