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
		GameObject ship = PrefabManager.get_instance ().InstantiatePrefab (PrefabManager.get_instance ().patrolBoat, realLocation);
	}

	protected override void InitializeShip ()
	{
		this.moveCommand = true;
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
