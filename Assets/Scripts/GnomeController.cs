using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(CharacterController))]
public class GnomeController : MonoBehaviour
{
    public bool isActive;
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
    public PickupDetection pd;
    public GameObject carrying {get; private set;}
    private Vector3 throwDirection;
    void Start()
    {
        controller = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.R))
        {
            // reset the level
            //could add in a wait here but i dont feel like making a coroutine just for that lol
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (isActive)
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
            throwDirection = transform.forward * 7;
            throwDirection.y = 7;
            
            if (moveDirection.magnitude > .2f)
            {
                float rotationAngle = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
                float smoothAngle = Mathf.SmoothDampAngle(transform.eulerAngles.y, rotationAngle, ref currentVelocity, smoothSpeed);
                transform.rotation = Quaternion.Euler(0f, smoothAngle, 0f);
                if (runprt.isStopped && controller.isGrounded)
                {
                    runprt.Play();
                }
            }
            else
            {
                runprt.Stop();
            }

            if (carrying)
            {
                carrying.transform.position = new Vector3(transform.position.x, transform.position.y + 2.75f, transform.position.z);
                carrying.transform.rotation = transform.rotation;
            }

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!carrying && pd.getTarget() != null)
                {
                    carrying = pd.getTarget();
                    carrying.GetComponent<Rigidbody>().isKinematic = true;
                    carrying.GetComponent<Collider>().enabled = false;
                }
                else if (carrying)
                {
                    carrying.GetComponent<Rigidbody>().isKinematic = false;
                    carrying.GetComponent<Collider>().enabled = true;
                    carrying.GetComponent<Rigidbody>().linearVelocity = throwDirection;
                    carrying = null;
                }
            }
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
            runprt.Stop();
        }
    }
}
