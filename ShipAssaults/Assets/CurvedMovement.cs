using UnityEngine;
using System.Collections;

public class CurvedMovement : MonoBehaviour {

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		Vector3 tmpPos = renderer.transform.position;
		tmpPos.y += 0.01f;
		//Increase
		renderer.transform.position = tmpPos;
	}
}
