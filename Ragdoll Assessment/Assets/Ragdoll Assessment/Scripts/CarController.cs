using UnityEngine;
using System.Collections;
using System.Collections.Generic;
    
public class CarController : MonoBehaviour 
{
	public AxleInfo frontWheel; // 关于每个轴的信息
	public AxleInfo backWheel;
	public float maxMotorTorque; // 电机可对车轮施加的最大扭矩
	public float maxSteeringAngle; // 车轮的最大转向角
        
	// 查找相应的可视车轮
	// 正确应用变换
	public void ApplyLocalPositionToVisuals(WheelCollider collider)
	{
		if (collider.transform.childCount == 0) 
		{
			return;
		}
     
		Transform visualWheel = collider.transform.GetChild(0);
     
		Vector3 position;
		Quaternion rotation;
		collider.GetWorldPose(out position, out rotation);
     
		visualWheel.transform.position = position;
		visualWheel.transform.localRotation = rotation;
	}
	
	public void FixedUpdate()
	{
		float motor = maxMotorTorque * Input.GetAxisRaw("Vertical");
		float steering = maxSteeringAngle * Input.GetAxisRaw("Horizontal");
            

		if (frontWheel.steering) 
		{
			frontWheel.leftWheel.steerAngle = steering;
			frontWheel.rightWheel.steerAngle = steering;
		}
			
		if (frontWheel.motor) 
		{ 
			frontWheel.leftWheel.motorTorque = motor; 
			frontWheel.rightWheel.motorTorque = motor;
		}
		ApplyLocalPositionToVisuals(frontWheel.leftWheel);
		ApplyLocalPositionToVisuals(frontWheel.rightWheel);
		ApplyLocalPositionToVisuals(backWheel.leftWheel);
		ApplyLocalPositionToVisuals(backWheel.rightWheel);
	}
}
    
[System.Serializable]
public class AxleInfo 
{
	public WheelCollider leftWheel;
	public WheelCollider rightWheel;
	public bool motor; // 此车轮是否连接到电机？
	public bool steering; // 此车轮是否施加转向角？
}