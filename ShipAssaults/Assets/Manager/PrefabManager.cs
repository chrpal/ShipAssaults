﻿using UnityEngine;
using System.Collections;

public class PrefabManager {

	protected static PrefabManager instance = null;
	public GameObject patrolBoat = null;
	public GameObject middleClassBattleShip = null;

	protected void Initialize()
	{
		patrolBoat = GameObject.Find ("PatrolBoat");
		middleClassBattleShip = GameObject.Find ("MiddleClassBattleShip");
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
