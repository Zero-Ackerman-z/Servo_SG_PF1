using UnityEngine;

public class VehicleController : MonoBehaviour
{

    [Header("Movement Settings")]
    public float moveSpeed = 10f;
    public float turnSpeed = 70f; 
    [Header("Wheel Settings (Optional)")]

    private float forwardInput;
    private float turnInput;
    private Rigidbody rb;
    void Start()
    {
        rb = GetComponent<Rigidbody>();
       
    }
    void Update()
    {
        forwardInput = Input.GetAxis("Vertical"); // W/S o flechas arriba/abajo
        turnInput = Input.GetAxis("Horizontal"); // A/D o flechas izquierda/derecha

        MoveVehicle();
        TurnVehicle();

       
    }

    private void MoveVehicle()
    {
        transform.Translate(Vector3.forward * -forwardInput * moveSpeed * Time.deltaTime);
    }

    private void TurnVehicle()
    {
        transform.Rotate(Vector3.up, turnInput * turnSpeed * Time.deltaTime);
    }


}
