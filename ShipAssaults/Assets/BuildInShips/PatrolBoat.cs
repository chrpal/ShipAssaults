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
		renderer.transform.localScale = new Vector3 (1f, 0.5f, 1);

		//Load Patrolboat properties
		this.maxRotation = 0.9f;
		this.maxTranslation = 0.015f;

		this.moveCommand = true;
		this.targetLocation = new Vector3 (-2, -5, renderer.transform.position.z);
	}

	// Use this for initialization
	protected override void Start () {
		base.Start ();
	}

	protected override void AnimateShipMotor()
	{
		Vector3 currentPos = renderer.transform.position;
		Vector3 motorLocation = currentPos;
		motorLocation.x -= 1;
		motorLocation.y -= 1;

		//ParticleManager.get_instance ().instantiateParticleSystem ("SimpleSplash", motorLocation);
	}

	// Update is called once per frame
	protected override void Update () {
		base.Update ();
	}
}
