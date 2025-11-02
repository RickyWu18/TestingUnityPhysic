using UnityEngine;

public class Gravity : MonoBehaviour
{
    public float gravity = -9.81f;
    public float mass = 1.0f;
    public Vector3 initialPosition;
    public float amplitude = 5f;
    public float period = 2f;
    public enum IntegrationMethod
    {
        Euler,
        RK4,
        Theory,
    }
    public IntegrationMethod integrationMethod = IntegrationMethod.RK4;

    private struct State
    {
        public Vector3 position, velocity;

        public State(Vector3 position, Vector3 velocity)
        {
            this.position = position;
            this.velocity = velocity;
        }

        public static State operator +(State a, State b)
        {
            return new State
            {
                position = a.position + b.position,
                velocity = a.velocity + b.velocity
            };
        }

        public static State operator *(float scalar, State a)
        {
            return new State
            {
                position = scalar * a.position,
                velocity = scalar * a.velocity
            };
        }

        public static State operator *(State a, float scalar)
        {
            return scalar * a;
        }

        public static State operator /(State a, float scalar)
        {
            return new State
            {
                position = a.position / scalar,
                velocity = a.velocity / scalar
            };
        }
    }
    private State state;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        state = new State(initialPosition, Vector3.zero);
    }

    // Update is called once per frame
    void Update()
    {
    }

    void FixedUpdate()
    {
        float dt = Time.fixedDeltaTime;
        Vector3 externalForce = new Vector3(0, Mathf.Cos(Time.time * (2 * Mathf.PI / period)) * amplitude, 0);
        Vector3 cancelGravityForce = new Vector3(0, -mass * gravity, 0);
        Vector3 gravityForce = new Vector3(0, mass * gravity, 0);
        Vector3 acceleration = (gravityForce  + cancelGravityForce + externalForce) / mass;

        switch (integrationMethod)
        {
            case IntegrationMethod.Euler:
                EulerStep(EquationsOfMotion, Time.time, dt);
                break;
            case IntegrationMethod.RK4:
                RK4Step(EquationsOfMotion, Time.time, dt);
                break;
            case IntegrationMethod.Theory:
                TheoreticalPosition(Time.time);
                break;
        }

        transform.position = (Vector3)state.position;
    }

    private State EquationsOfMotion(float t, State x)
    {
        Vector3 externalForce = new Vector3(0, Mathf.Cos(t * (2 * Mathf.PI / period)) * amplitude, 0);
        Vector3 cancelGravityForce = new Vector3(0, -mass * gravity, 0);
        Vector3 gravityForce = new Vector3(0, mass * gravity, 0);
        Vector3 acceleration = (gravityForce  + cancelGravityForce + externalForce) / mass;

        // return state derivative
        return new State(x.velocity, acceleration);
    }

    private void RK4Step(System.Func<float, State, State> ode_func, float time, float dt)
    {
        State k1 = ode_func(time, state);
        State k2 = ode_func(time + dt / 2, state + dt / 2 * k1);
        State k3 = ode_func(time + dt / 2, state + dt / 2 * k2);
        State k4 = ode_func(time + dt, state + dt * k3);

        state += dt / 6 * (k1 + 2 * k2 + 2 * k3 + k4);
    }

    private void EulerStep(System.Func<float, State, State> ode_func, float time, float dt)
    {
        State derivative = ode_func(time, state);

        state += derivative * dt;
    }

    private void TheoreticalPosition(float time)
    {
        Vector3 homogeneousSolution = new Vector3(0, -0.5f * gravity * time * time, 0);
        Vector3 cancelingSolution = new Vector3(0, 0.5f * gravity * time * time, 0);

        float omega = 2 * Mathf.PI / period;
        Vector3 particularSolution = new Vector3(0, amplitude / (mass * omega * omega) - amplitude / (mass * omega * omega) * Mathf.Cos(omega * time), 0);

        state = new State
        {
            position = initialPosition + homogeneousSolution + cancelingSolution + particularSolution,
            velocity = new Vector3(0, gravity * time, 0)
        };
    }
}
