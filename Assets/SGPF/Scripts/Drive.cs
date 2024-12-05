using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Drive : MonoBehaviour
{
    public WheelCollider[] frontWheels; 
    public WheelCollider[] rearWheels;
    public GameObject[] wheelModels;   
    public float motorTorque = 200f;   
    public float brakeTorque = 500f;   
    public float steeringAngle = 30f;  

    private float currentAccel;
    private float currentSteer;

    void Start()
    {
    }
    void Update()
    {
        currentAccel = Input.GetAxis("Vertical");  
        currentSteer = Input.GetAxis("Horizontal");  

        HandleSteering();
        HandleMotor();
        UpdateWheelPositions();
    }

    void HandleMotor()
    {
        float motorForce = currentAccel * motorTorque;

        foreach (WheelCollider wheel in rearWheels)
        {
            wheel.motorTorque = motorForce;
        }

        if (currentAccel < 0)
        {
            foreach (WheelCollider wheel in rearWheels)
            {
                wheel.brakeTorque = -motorForce; 
            }
        }
        else
        {
            foreach (WheelCollider wheel in rearWheels)
            {
                wheel.brakeTorque = 0; 
            }
        }
    }

    void HandleSteering()
    {
        float steer = currentSteer * steeringAngle;

        foreach (WheelCollider wheel in frontWheels)
        {
            wheel.steerAngle = steer;
        }
    }

    void UpdateWheelPositions()
    {
        for (int i = 0; i < frontWheels.Length; i++)
        {
            WheelCollider wheelCollider = frontWheels[i];
            Transform wheelTransform = wheelModels[i].transform; 
            UpdateWheelPose(wheelCollider, wheelTransform);
        }

        for (int i = 0; i < rearWheels.Length; i++)
        {
            WheelCollider wheelCollider = rearWheels[i];
            Transform wheelTransform = wheelModels[i + frontWheels.Length].transform;  
            UpdateWheelPose(wheelCollider, wheelTransform);
        }
    }

    void UpdateWheelPose(WheelCollider wheelCollider, Transform wheelTransform)
    {
        Vector3 pos;
        Quaternion rot;
        wheelCollider.GetWorldPose(out pos, out rot);

        wheelTransform.position = pos;
        wheelTransform.rotation = rot;
    }
}
