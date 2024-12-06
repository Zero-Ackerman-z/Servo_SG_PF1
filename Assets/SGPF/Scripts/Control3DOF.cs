using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Control3DOF : MonoBehaviour
{
    public ConfigurableJoint pistonA; 
    public ConfigurableJoint pistonB;  
    public ConfigurableJoint pistonRot;  

    public float velocidadLinealA = 5.0f;  
    public float velocidadLinealB = 5.0f;  
    public float velocidadRotacional = 20.0f;  
    public float distanciaLimite = 1.0f;  
    public float inclinacionMaxima = 45f; 

    void Start()
    {
        if (pistonA == null || pistonB == null || pistonRot == null)
        {
            Debug.LogError("Faltan referencias a Configurable Joints!");
            return;
        }
        SoftJointLimit limitA = new SoftJointLimit();
        limitA.limit = distanciaLimite;
        pistonA.linearLimit = limitA;

        SoftJointLimit limitB = new SoftJointLimit();
        limitB.limit = distanciaLimite;
        pistonB.linearLimit = limitB;

        pistonRot.angularXMotion = ConfigurableJointMotion.Limited;
        pistonRot.angularYMotion = ConfigurableJointMotion.Limited;
        pistonRot.angularZMotion = ConfigurableJointMotion.Limited;

        SoftJointLimit angularLimit = new SoftJointLimit();
        angularLimit.limit = inclinacionMaxima;

        pistonRot.lowAngularXLimit = angularLimit;
        pistonRot.highAngularXLimit = angularLimit;

        SoftJointLimit angularLimitZ = new SoftJointLimit();
        angularLimitZ.limit = inclinacionMaxima;

        pistonRot.angularZLimit = angularLimitZ;
    }
    void Update()
    {
        MoveOnA(velocidadLinealA);
        MoveOnB(velocidadLinealB);
        RotateRot(velocidadRotacional);
    }
    void MoveOnA(float velocidad)
    {
        pistonA.targetPosition = new Vector3(velocidad * Time.deltaTime, 0, 0);
    }
    void MoveOnB(float velocidad)
    {
        pistonB.targetPosition = new Vector3(0, 0, velocidad * Time.deltaTime);
    }
    void RotateRot(float velocidad)
    {
        Vector3 rotacion = transform.localEulerAngles;

        if (rotacion.x > 180) rotacion.x -= 360;
        if (rotacion.z > 180) rotacion.z -= 360;

        rotacion.x = Mathf.Clamp(rotacion.x, -inclinacionMaxima, inclinacionMaxima);
        rotacion.z = Mathf.Clamp(rotacion.z, -inclinacionMaxima, inclinacionMaxima);

        pistonRot.targetRotation = Quaternion.Euler(rotacion.x, 0, rotacion.z);
    }
}
