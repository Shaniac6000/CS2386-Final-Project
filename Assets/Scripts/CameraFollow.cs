using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    [SerializeField] private Transform player;

    private Vector3 velocity = Vector3.zero;

    private float camTime = .15f;

    Vector3 offset;

    void Start()
    {
        if (!player)
        {
            player = GameObject.FindGameObjectWithTag("Player").transform;
        }
        offset = transform.position - player.position;
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 target = player.position + offset;
        transform.position = Vector3.SmoothDamp(transform.position, target, ref velocity, camTime);
    }
}
