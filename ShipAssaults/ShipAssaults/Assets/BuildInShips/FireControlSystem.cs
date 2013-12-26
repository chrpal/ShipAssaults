using UnityEngine;
using System.Collections;

public class FireControlSystem : MonoBehaviour {


	public IGunTurret [] turrents;

	public GameObject target;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
		turrents[0].setTarget(target.transform.position);
		turrents[1].setTarget(target.transform.position);
	     
	}
}
