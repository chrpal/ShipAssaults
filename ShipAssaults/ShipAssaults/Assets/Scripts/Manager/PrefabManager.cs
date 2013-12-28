using UnityEngine;
using System.Collections.Generic;

public class PrefabManager {

	protected static PrefabManager instance = null;

	public Dictionary<string, GameObject> preFabs;

	/*public GameObject patrolBoat = null;
	public GameObject middleClassBattleShip = null;*/



	protected void Initialize()
	{
		this.preFabs = new Dictionary<string, GameObject> ();

		this.preFabs.Add ("PatrolBoat", Resources.Load<GameObject> ("Prefabs/PatrolBoat"));
		this.preFabs.Add ("MiddleClassBattleShip", Resources.Load<GameObject> ("Prefabs/Ships/Haruna"));
	}

	public GameObject InstantiatePrefab(string name, Vector3 location)
	{
		GameObject result = null;
		GameObject obj = null;
		if (this.preFabs.ContainsKey (name)) 
		{
			obj = this.preFabs[name];
		}
		if (obj != null) 
		{
			result = MonoBehaviour.Instantiate (obj, location, Quaternion.identity) as GameObject;
		}
		return result;
	}

	public GameObject InstantiatePrefab(GameObject obj, Vector3 location)
	{
		GameObject result = null;
		if (obj != null) 
		{
			result = MonoBehaviour.Instantiate (obj, location, Quaternion.identity) as GameObject;
		}
		return result;
	}

	public static PrefabManager get_instance()
	{
		if (instance == null) 
		{
			instance = new PrefabManager();
			instance.Initialize();
		}
		return instance;
	}
}
