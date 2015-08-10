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
	

	public DriveTrain DriveTrain = DriveTrain.FrontWheelDrive;
	public float GasForce = 100;

	public float MaxTurnAngle = 20.0f;

	private float currentAngle = 0;

	public void Start ()
	{
		currentAngle = 0;
	}


	public void FixedUpdate ()
	{

		//Gas needs to continually accelerate to a max velocity
		float Gas = Input.GetAxis ("Vertical") * Time.fixedDeltaTime * GasForce; 

		float turnAngle = Input.GetAxis ("Horizontal") * MaxTurnAngle / 2;

		switch (DriveTrain) {
		case DriveTrain.FrontWheelDrive:
			FRWheelCollider.motorTorque = Gas;
			FLWheelCollider.motorTorque = Gas;
			break;
		case DriveTrain.RearWheelDrive:
			break;
		case DriveTrain.AllWheelDrive:
			break;
		}

		FRWheelCollider.steerAngle = turnAngle; 
		FLWheelCollider.steerAngle = turnAngle;
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
