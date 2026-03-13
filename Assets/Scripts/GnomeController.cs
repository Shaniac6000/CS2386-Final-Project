using System.Collections;
using UnityEditor.ShaderGraph.Internal;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GnomeController : MonoBehaviour
{
    public float speed;
    public float jumpHeight;
    public float airControl = 10f;
    public float gravity = 9.81f;
    private static Vector3 isoRight = new Vector3( 1, 0, -1).normalized; // D
    private static Vector3 isoForward = new Vector3( 1, 0,  1).normalized; // W
    private CharacterController controller;
    private Vector3 input, moveDirection;
    private float currentVelocity;
    private float smoothSpeed = .1f;
    public ParticleSystem runprt;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        // get input
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // input vector
        input = isoRight * moveHorizontal + isoForward * moveVertical;

        if (input.magnitude > 1f)
        {
            input.Normalize();
        }
            
        Jump();

        moveDirection.y -= gravity * Time.deltaTime;
        controller.Move(moveDirection * speed * Time.deltaTime);
        
        if (moveDirection.magnitude > .1f)
        {
            float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
            float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref currentVelocity, smoothSpeed);
            transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
            if (runprt.isStopped)
            {
                runprt.Play();
            }
        }
        else
        {
            runprt.Stop();
        }
    }

    void Jump()
    {
        if (controller.isGrounded)
        {
            moveDirection = input;
            // jump
            if (Input.GetButton("Jump"))
            {
                moveDirection.y = Mathf.Sqrt(2 * jumpHeight * gravity);
            }
            else
            {
                moveDirection.y =  0.0f;
            }
        }
        else
        {
            // midair
            input.y = moveDirection.y;
            moveDirection = Vector3.Lerp(moveDirection, input, airControl * Time.deltaTime);
        }
    }
}
