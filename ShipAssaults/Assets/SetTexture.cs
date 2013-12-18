using UnityEngine;
using System.Collections;

public class SetTexture : MonoBehaviour {

	public Texture2D texture = null;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		renderer.material.mainTexture = texture;
		var shader = Shader.Find ("Unlit/Transparent");
		renderer.material.shader = shader;
	}
}
