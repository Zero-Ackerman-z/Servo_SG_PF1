using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VehicleMotorController : MonoBehaviour
{
   public ConfigurableJoint frontLeftWheel;
    public ConfigurableJoint frontRightWheel;
    public ConfigurableJoint rearWheel;

    // Velocidades y parámetros de control
    public float forwardSpeed = 10f;    // Velocidad de avance (m/s)
    public float turnSpeed = 90f;       // Velocidad de giro (grados/segundo)

    private float currentSpeed = 0f;    // Velocidad actual
    private float currentTurn = 0f;     // Ángulo de giro

    void Update()
    {
        // Actualizar los valores de los motores de las ruedas
        UpdateMotorValues(currentSpeed, currentTurn);

        // Aplicar las fuerzas correspondientes a los Joints para el movimiento
        ApplyMotorForces(frontLeftWheel, frontRightWheel, rearWheel, currentSpeed, currentTurn);
    }

    // Función para actualizar las velocidades de los motores (Configurable Joints)
    void UpdateMotorValues(float speed, float turn)
    {
        // El giro afecta las ruedas delanteras más que las traseras
        frontLeftWheel.targetRotation = Quaternion.Euler(0, turn, 0); // Giro de la rueda izquierda
        frontRightWheel.targetRotation = Quaternion.Euler(0, turn, 0); // Giro de la rueda derecha

        // El movimiento hacia adelante o atrás afecta las ruedas
        // Puedes ajustar la fuerza aplicada en los ejes X (traslación) y Z (giro)
        frontLeftWheel.targetPosition = new Vector3(0, 0, speed);
        frontRightWheel.targetPosition = new Vector3(0, 0, speed);
        rearWheel.targetPosition = new Vector3(0, 0, speed);
    }

    // Aplicar las fuerzas a los Joints para simular el movimiento
    void ApplyMotorForces(ConfigurableJoint frontLeft, ConfigurableJoint frontRight, ConfigurableJoint rear, float speed, float turn)
    {
        // Aplica una fuerza constante a las ruedas delanteras y traseras para mover el vehículo
        // Ajusta las propiedades de cada Configurable Joint según el movimiento deseado

        // Aplicar la velocidad de avance a las ruedas delanteras
        ApplyForceToWheel(frontLeft, speed, turn);
        ApplyForceToWheel(frontRight, speed, turn);
        ApplyForceToWheel(rear, speed, turn);
    }

    // Función auxiliar para aplicar fuerza a cada rueda
    void ApplyForceToWheel(ConfigurableJoint wheelJoint, float speed, float turn)
    {
        // Definir las fuerzas aplicadas en los ejes X (para avance) y Z (para giro)
        JointDrive xDrive = new JointDrive();
        xDrive.positionSpring = 100f;  // Ajustar la rigidez de la suspensión
        xDrive.positionDamper = 10f;   // Ajustar el amortiguamiento

        wheelJoint.xDrive = xDrive;

        // Fuerzas para el movimiento hacia adelante
        wheelJoint.targetPosition = new Vector3(speed, wheelJoint.targetPosition.y, wheelJoint.targetPosition.z);

        // Si es necesario, puedes aplicar torques en los ejes Y para el giro
        JointDrive zDrive = new JointDrive();
        zDrive.positionSpring = 100f;
        zDrive.positionDamper = 10f;

        wheelJoint.zDrive = zDrive;
    }
}