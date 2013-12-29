using UnityEngine;
using System.Collections;

public class FieldInteraction : MonoBehaviour {

	static public float movementStrength = 0.5f;
	static public float zoomLevel = 0.5f;
	static Camera MainCam;
	static GameObject MainPlane;
	static bool rightMouseDown = false;
	static Vector3 lastMousePosition = new Vector3(0,0, 0);

	static FieldInteraction instance = null; 

	static public float maxCamZoom = -15.5f;
	static public float minCamZoom = -2.0f;

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
		MainPlane = GameObject.Find ("MainPlane");
		lastMousePosition = Input.mousePosition;
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

		rightMouseDown = Input.GetMouseButton (1);
		Vector3 deltaMouse = lastMousePosition - Input.mousePosition;

		if (Input.GetMouseButtonDown (0)) 
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
			MoveCameraRel(deltaMouse.x*0.1f,deltaMouse.y*0.1f,0);
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
			MainCam.transform.position = Vector3.Lerp (MainCam.transform.position, camPosition, movementStrength);
		}
	}
}
