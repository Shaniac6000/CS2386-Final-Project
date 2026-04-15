using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform target;

    private Vector3 velocity = Vector3.zero;

    private float camTime = .15f;

    Vector3 offset;
    private float scrollSpeed = 2000;

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

        float scroll = Input.GetAxis("Mouse ScrollWheel");
        GetComponent<Camera>().orthographicSize -= scroll * Time.deltaTime * scrollSpeed;
        GetComponent<Camera>().orthographicSize = Mathf.Clamp(GetComponent<Camera>().orthographicSize, 15, 40);
    }

    public void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }
}
