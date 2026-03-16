using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 velocity = Vector3.zero;

    private float camTime = .15f;

    Vector3 offset;

    void Start()
    {
        if (!target)
        {
            target = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = transform.position - target.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 pos = target.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, pos, ref velocity, camTime);
    }
}
