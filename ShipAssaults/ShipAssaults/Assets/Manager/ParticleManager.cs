using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ParticleManager : MonoBehaviour {

	Dictionary<string, GameObject> particleSystems;
	static ParticleManager instance = null;

	public static ParticleManager get_instance()
	{
		if (instance == null) 
		{
			instance = new ParticleManager();
			instance.Start();
		}
		return instance;
	}

	// Use this for initialization
	void Start () {
		particleSystems = new Dictionary<string, GameObject>();
		particleSystems.Add ("ArtillerySplash", GameObject.Find("ArtilleryWaterSplash1"));
		particleSystems.Add ("SimpleSplash", GameObject.Find("SimpleWaterSplash1"));
		particleSystems.Add ("SmallSplash", GameObject.Find("SmallWaterSplash1"));
		instance = this;
	}

	// Update is called once per frame
	void Update () {
	
	}

	public void instantiateParticleSystem(string name, Vector3 location)
	{
		this.instantiateParticleSystem (name, location, 1.0f);
	}

	public void instantiateParticleSystem(string name, Vector3 location, float timeToDestroy)
	{
		if (this.particleSystems.ContainsKey(name))
		{
			GameObject go = Instantiate (this.particleSystems[name], location, Quaternion.identity) as GameObject;
			Destroy(go, timeToDestroy);
		}
	}
}
