using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Control3DOF : MonoBehaviour
{
    // Referencias a los Joints
    public ConfigurableJoint jointX;  // Joint para el eje X
    public ConfigurableJoint jointZ;  // Joint para el eje Z
    public ConfigurableJoint jointRot;  // Joint para la rotación en el eje X o Z

    // Valores de control
    public float velocidadLinealX = 5.0f;  // Velocidad en el eje X
    public float velocidadLinealZ = 5.0f;  // Velocidad en el eje Z
    public float velocidadRotacional = 20.0f;  // Velocidad de rotación en el eje X o Z
    public float distanciaLimite = 1.0f;  // Limite de desplazamiento para el movimiento de los ejes

    void Start()
    {
        // Asegurarse de que los joints están correctamente configurados
        if (jointX == null || jointZ == null || jointRot == null)
        {
            Debug.LogError("Faltan referencias a Configurable Joints!");
            return;
        }

        // Configuración de límites de movimiento para el jointX (Eje X)
        SoftJointLimit limitX = new SoftJointLimit();
        limitX.limit = distanciaLimite;
        jointX.linearLimit = limitX;

        // Configuración de límites de movimiento para el jointZ (Eje Z)
        SoftJointLimit limitZ = new SoftJointLimit();
        limitZ.limit = distanciaLimite;
        jointZ.linearLimit = limitZ;

        // Configuración del límite de rotación en el eje X o Z
        SoftJointLimit angularLimit = new SoftJointLimit();
        angularLimit.limit = 45f;  // Limitar la rotación en el eje X o Z
        jointRot.angularXLimit = angularLimit;
        jointRot.angularZLimit = angularLimit;
    }

    void Update()
    {
        // Controlar el movimiento en el eje X
        MoveOnX(velocidadLinealX);

        // Controlar el movimiento en el eje Z
        MoveOnZ(velocidadLinealZ);

        // Controlar la rotación en el eje X o Z
        RotateXorZ(velocidadRotacional);
    }

    void MoveOnX(float velocidad)
    {
        // Mover el vehículo en el eje X
        jointX.targetPosition = new Vector3(velocidad * Time.deltaTime, 0, 0);
    }

    void MoveOnZ(float velocidad)
    {
        // Mover el vehículo en el eje Z
        jointZ.targetPosition = new Vector3(0, 0, velocidad * Time.deltaTime);
    }

    void RotateXorZ(float velocidad)
    {
        // Rotar el vehículo en el eje X o Z
        jointRot.targetRotation = Quaternion.Euler(velocidad * Time.deltaTime, 0, 0);  // Rotación sobre X
        // Para rotar sobre el eje Z, puedes usar la siguiente línea en lugar de la anterior
        // jointRot.targetRotation = Quaternion.Euler(0, 0, velocidad * Time.deltaTime);  // Rotación sobre Z
    }
}
