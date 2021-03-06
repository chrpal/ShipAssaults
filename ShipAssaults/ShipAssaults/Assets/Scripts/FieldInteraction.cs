﻿using UnityEngine;
using System.Collections;

public class FieldInteraction : MonoBehaviour {

	static public float movementRatio = 0.5f;
	static public float zoomLevel = 5f;
	static Camera MainCam;
	static GameObject MainPlane;

	static bool leftMouseClicked = false;
	static bool leftMouseDown = false;
	static bool rightMouseDown = false;
	static bool rightMouseClicked = false;

	static Vector3 lastMousePosition = new Vector3(-1000.0f, -1000.0f, 0.0f);

	static FieldInteraction instance = null; 

	static public float maxCamZoom = -120.0f;
	static public float minCamZoom = -7.0f;

	public static FieldInteraction get_instance()
	{
		if (instance == null)
		{
			instance = new FieldInteraction();
			instance.Start();
		}
		return instance;
	}


	// Use this for initialization
	void Start () {
		MainCam = GameObject.Find ("MainCam").camera;
		MainPlane = GameObject.Find ("Water");
		lastMousePosition = Input.mousePosition;
	}

	void OnGUI()
	{
		this.DrawQuad (new Rect (0, 0, 50, 50), Color.green);
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetAxis ("Mouse ScrollWheel") < 0) 
		{
			MoveCameraRel(0,0,zoomLevel);
		}

		if (Input.GetAxis ("Mouse ScrollWheel") > 0) 
		{
			MoveCameraRel(0,0,-zoomLevel);
		}

		leftMouseDown = Input.GetMouseButton (0);
		leftMouseClicked = Input.GetMouseButtonDown(0);

		rightMouseDown = Input.GetMouseButton (1);
		rightMouseClicked = Input.GetMouseButtonDown (1);

		Vector3 deltaMouse = lastMousePosition - Input.mousePosition;

		if (leftMouseClicked) 
		{
			Vector3 hitPoint;
			bool selected = ObjectSelected(MainPlane,out hitPoint);
			if (selected)
			{
				//ParticleManager.get_instance().instantiateParticleSystem("SimpleSplash",new Vector3(hitPoint.x,hitPoint.y,0));
				PrefabManager.get_instance().InstantiatePrefab("MiddleClassBattleShip",hitPoint);
			}
		}

		if (rightMouseDown) 
		{
			MoveCameraRel(deltaMouse.x*movementRatio,deltaMouse.y*movementRatio,0);
		}

		lastMousePosition = Input.mousePosition;
	}

	public bool ObjectSelected(GameObject obj)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0.0f;
		Plane plane = new Plane(new Vector3(obj.transform.position.x,obj.transform.position.y,-1),0);
		bool result = plane.Raycast(ray, out distance);
		return result;
	}

	public bool ObjectSelected(GameObject obj, out Vector3 hitPoint)
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		float distance = 0.0f;
		Plane plane = new Plane(new Vector3(obj.transform.position.x,obj.transform.position.y,-1),0);
		bool result = plane.Raycast(ray, out distance);
		hitPoint = new Vector3 ();
		if (result) 
		{
			hitPoint = ray.GetPoint(distance);
		}
		return result;
	}

	public void MoveCameraRel(float dx, float dy, float dz)
	{
		float x = MainCam.transform.position.x + dx;
		float y = MainCam.transform.position.y + dy;
		float z = MainCam.transform.position.z + dz;
		MoveCameraAbs (x, y, z);
	}

	public void MoveCameraAbs(float x, float y, float z)
	{

		if (Mathf.Abs(z) >= Mathf.Abs(minCamZoom) && Mathf.Abs(z) <= Mathf.Abs(maxCamZoom)) 
		{
			Vector3 camPosition = new Vector3 (x, y, z);
			MainCam.transform.position = Vector3.Lerp (MainCam.transform.position, camPosition, 1.0f);
		}
	}

	void DrawSelectionRect(Rect position, Color color) {
		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0,color);
		texture.Apply();
		GUI.skin.box.normal.background = texture;
		GUI.Box(position, GUIContent.none);
	}
}
