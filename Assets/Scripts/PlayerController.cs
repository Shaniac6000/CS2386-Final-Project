using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

[RequireComponent(typeof(Rigidbody))]
public class PlayerController : MonoBehaviour
{
    public float speed;

    public float jumpForce;

    private Rigidbody rb;

    private bool isGrounded;
    private Vector3 forward, right;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
    }

    // Update is called once per frame
    void Update()
    {
        Jump();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        Vector3 forwardMove = forward * Input.GetAxis("Vertical");
        Vector3 rightMove = right * Input.GetAxis("Horizontal");
        Vector3 movement = (forwardMove + rightMove).normalized;

        rb.AddForce(movement * speed);
    }

    void Jump()
    {
        if (Input.GetKeyDown(KeyCode.Space) && isGrounded)
        {

            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            isGrounded = false;
        }
    }

    void OnCollisionEnter(Collision collision)
    {   
        ContactPoint contact = collision.contacts[0];

        if (contact.normal.y > .5f)
        {
            isGrounded = true;
        }
    }
}
