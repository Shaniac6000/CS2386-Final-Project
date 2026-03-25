using UnityEngine;

public class LockBehavior : MonoBehaviour
{
    public bool locked = true;
    public AudioClip sfx;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            locked = false;
            rb.isKinematic = false;
            if (sfx)
                AudioSource.PlayClipAtPoint(sfx, transform.position);
        }
    }
}
