using UnityEngine;
using System.Collections;

public class PatrolBoat : IShip {

	public static void CreateShip()
	{
		Vector2 location = new Vector2(0,0);
		CreateShip (location);
	}

	public static void CreateShip(Vector2 location)
	{
		Vector3 realLocation = new Vector3 (location.x, location.y, -0.1f);
		GameObject ship = PrefabManager.get_instance ().InstantiatePrefab (PrefabManager.get_instance ().shipQuad, realLocation);
		ship.AddComponent ("PatrolBoat");
	}

	protected override void InitializeShip ()
	{
		//Load texture
		Texture2D tex = (Texture2D)Resources.LoadAssetAtPath ("Assets/Resources/Images/patrolboat", typeof(Texture2D));
		renderer.material.mainTexture = tex;

		//Set shader
		Shader shader = Shader.Find ("Unlit/Transparent");
		renderer.material.shader = shader;


		//Load Patrolboat properties
		this.maxRotation = 0.1f;
		this.maxTranslation = 0.015f;

		this.moveCommand = true;
		this.targetLocation = new Vector3 (10, 10, renderer.transform.position.z);
	}

	// Use this for initialization
	void Start () {
		this.InitializeShip ();
	}

	protected override void AnimateShipMotor()
	{
		Vector3 currentPos = renderer.transform.position;
		Vector3 motorLocation = currentPos;
		motorLocation.x -= 1*Mathf.Cos(magnitude);
		motorLocation.y -= 1*Mathf.Sin(magnitude);

		ParticleManager.get_instance ().instantiateParticleSystem ("SimpleSplash", motorLocation);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
