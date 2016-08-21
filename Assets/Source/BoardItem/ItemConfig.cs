using UnityEngine;
using System.Collections;

public enum ItemType {
	None = 0,
	Person = 1,
	Crystal = 2,
	Dst = 3,
	Wind = 10,
	Ice = 20,
	Barrier = 30,
	Stone = 40,
	Wall = 50,
}

public class ItemConfig {
	public int ID { get; private set; }
	public ItemType Type { get; private set; }

	public ItemConfig (int id) {
		ID = id;
		if (id >= (int)ItemType.Wall)
			Type = ItemType.Wall;
		else if (id >= (int)ItemType.Stone)
			Type = ItemType.Stone;
		else if (id >= (int)ItemType.Barrier)
			Type = ItemType.Barrier;
		else if (id >= (int)ItemType.Ice)
			Type = ItemType.Ice;
		else if (id >= (int)ItemType.Wind)
			Type = ItemType.Wind;
		else 
			Type = (ItemType)id;
	}
}
