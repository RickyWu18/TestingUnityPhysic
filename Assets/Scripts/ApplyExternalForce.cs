using UnityEngine;

public class ApplyExternalForce : MonoBehaviour
{
    public Vector3 initialPosition;
    public Vector3 currentVelocity;
    public float amplitude = 5f;
    public float period = 2f;

    private Rigidbody rb;
    private float gravity;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        gravity = Mathf.Abs(Physics.gravity.y);
        transform.position = initialPosition;
    }

    // Update is called once per frame
    void Update()
    {
        // currentVelocity = rb.linearVelocity;
        // Debug.Log("Current Velocity: " + currentVelocity);
    }

    void FixedUpdate()
    {
        if (rb == null) return;

        Vector3 sinuousForce = new Vector3(0, Mathf.Cos(Time.time * (2 * Mathf.PI / period)) * amplitude, 0);
        Vector3 cancelGravityForce = new Vector3(0, rb.mass * gravity, 0);
        rb.AddForce(cancelGravityForce + sinuousForce, ForceMode.Force);
    }
}