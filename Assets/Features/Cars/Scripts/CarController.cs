using UnityEngine;
using System.Collections;

public enum DriveTrain
{
	FrontWheelDrive,
	RearWheelDrive,
	AllWheelDrive
}

public class CarController : MonoBehaviour
{
	public WheelCollider FRWheelCollider = null;
	public WheelCollider FLWheelCollider = null;
	public WheelCollider RLWheelCollider = null;
	public WheelCollider RRWheelCollider = null;

	public GameObject FRWheel = null;
	public GameObject FLWheel = null;
	public GameObject RLWheel = null;
	public GameObject RRWheel = null;


	public DriveTrain DriveTrain = DriveTrain.FrontWheelDrive;
	public float GasForce = 100;
	public float BreakForce = 10;
	public float MaxTurnAngle = 20.0f;

	private float currentAngle = 0;

	public void Start ()
	{
		currentAngle = 0;
	}


	public void FixedUpdate ()
	{

		//Gas needs to continually accelerate to a max velocity
		float gasBreak = Input.GetAxis ("Vertical") * Time.fixedDeltaTime; 
		float turnAngle = Input.GetAxis ("Horizontal") * MaxTurnAngle / 2;
        
		if (gasBreak > 0) {
			gasBreak *= GasForce;
			switch (DriveTrain) {
			case DriveTrain.FrontWheelDrive:
				FRWheelCollider.motorTorque = gasBreak;
				FLWheelCollider.motorTorque = gasBreak;
				break;
			case DriveTrain.RearWheelDrive:
				RRWheelCollider.motorTorque = gasBreak;
				RLWheelCollider.motorTorque = gasBreak;
				break;
			case DriveTrain.AllWheelDrive:
				FRWheelCollider.motorTorque = gasBreak;
				FLWheelCollider.motorTorque = gasBreak;
				RRWheelCollider.motorTorque = gasBreak;
				RLWheelCollider.motorTorque = gasBreak;
				break;
			}
		} else {
			gasBreak *= BreakForce;

			//apply break
			FRWheelCollider.brakeTorque = gasBreak;
			FLWheelCollider.brakeTorque = gasBreak;
			RRWheelCollider.brakeTorque = gasBreak;
			RLWheelCollider.brakeTorque = gasBreak;            
		}


		FRWheelCollider.steerAngle = turnAngle; 
		FLWheelCollider.steerAngle = turnAngle;

		applyVisuals ();
	}

	private void applyVisuals ()
	{
		applyVisuals (FLWheelCollider, FLWheel);
		applyVisuals (FRWheelCollider, FRWheel);
		applyVisuals (RLWheelCollider, RLWheel);
		applyVisuals (RRWheelCollider, RRWheel);
	}

	private void applyVisuals (WheelCollider collider, GameObject visual)
	{
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose (out position, out rotation);
		visual.transform.position = position;
		visual.transform.rotation = rotation;
	}

	private float clampAngle (float angle)
	{
		if (angle > MaxTurnAngle) {
			return MaxTurnAngle;
		} else if (angle < -MaxTurnAngle) {
			return -MaxTurnAngle;
		}

		return angle;
	}

}
