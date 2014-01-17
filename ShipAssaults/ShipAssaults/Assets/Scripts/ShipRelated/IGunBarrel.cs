using UnityEngine;
using System.Collections;

public class IGunBarrel : MonoBehaviour {

	//public GameObject barrel;
	public Transform projectileSpawnPoint;
	protected bool fireBurstAnimation = false;

	protected byte animationStatus = 0;


	// Use this for initialization
	void Start () {
	
	}

	protected virtual void Animate ()
	{
	}

	// Update is called once per frame
	void Update () {
		if (this.fireBurstAnimation == true)
		{
			this.Animate();
		}
	}

	public virtual void Fire()
	{
		this.fireBurstAnimation = true;
	}
}
