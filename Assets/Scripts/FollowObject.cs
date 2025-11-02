using UnityEngine;

public class FollowObject : MonoBehaviour
{
    public GameObject target;
    public Vector3 offset;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Application.runInBackground = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (target != null)
        {
            transform.position = target.transform.position + offset;
            // look at the target
            transform.LookAt(target.transform);
        }
    }
}
