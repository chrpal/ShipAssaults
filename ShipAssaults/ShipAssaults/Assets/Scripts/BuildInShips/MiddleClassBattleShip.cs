using UnityEngine;
using System.Collections;

public class MiddleClassBattleShip : IShip {
	
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
