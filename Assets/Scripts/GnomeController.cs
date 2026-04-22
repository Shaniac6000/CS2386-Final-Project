using Ink.Parsed;
using TMPro;
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
    private CharacterController controller;
    private Vector3 input, moveDirection;
    private float currentVelocity;
    private float smoothSpeed = .1f;
    public ParticleSystem runprt;
    public PickupDetection pd;
    public GameObject carrying {get; private set;}
    private bool isDragging;
    private Vector3 throwDirection;
    public TextMeshProUGUI grabIndicator;
    public Transform gnomeModel;
    public bool thrown = false;
    private TextMeshProUGUI deathText;
    private Vector3 forward, right;
    public AudioClip jump;
    public AudioClip pickup;
    public AudioClip throwing;
    public AudioClip death;
    private AudioSource source;
    private Animator animator;
    private DialogueManager dm;

    void Start()
    {
        controller = GetComponent<CharacterController>();
        deathText = GameObject.FindGameObjectWithTag("DeathText").GetComponent<TextMeshProUGUI>();
        forward = Camera.main.transform.forward;
        forward.y = 0;
        forward = Vector3.Normalize(forward);
        right = Quaternion.Euler(new Vector3(0, 90, 0)) * forward;
        source = GetComponent<AudioSource>();
        animator = GetComponentInChildren<Animator>();
        dm = FindFirstObjectByType<DialogueManager>();
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.R))
        {
            // reset the level
            //could add in a wait here but i dont feel like making a coroutine just for that lol
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
            Time.timeScale = 1;
        }
        if (isActive && !thrown)
        {
            if (GetComponent<TrappedGnome>())
            {
                gnomeModel.localPosition = new Vector3(0.0599999987f,0.270000011f,-0.0700000003f);
            }

            // get input
            float moveHorizontal = Input.GetAxis("Horizontal") ;
            float moveVertical = Input.GetAxis("Vertical");

            // input vector
            input = right * moveHorizontal + forward * moveVertical;

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

            if (Input.GetKeyDown(KeyCode.E))
            {
                if (!carrying && pd.getTarget() != null)
                {
                    source.clip = pickup;
                    source.Play();
                    carrying = pd.getTarget();
                    if (carrying.GetComponent<Rigidbody>())
                        carrying.GetComponent<Rigidbody>().isKinematic = true;
                    else if (carrying.GetComponent<CharacterController>())
                    {
                        carrying.GetComponent<CharacterController>().enabled = false;
                    }
                    carrying.GetComponent<Collider>().enabled = false;
                    isDragging = carrying.CompareTag("Draggable");
                    grabIndicator.enabled = true;
                    if (!isDragging)
                    {
                        grabIndicator.text = "Press E to Throw";
                    }
                    else
                    {
                        grabIndicator.text = "Press E to Drop";
                    }

                }
                else if (carrying)
                {
                    source.clip = throwing;
                    source.Play();
                    grabIndicator.enabled = false;
                    if (carrying.GetComponent<Rigidbody>())
                        carrying.GetComponent<Rigidbody>().isKinematic = false;
                    else if (carrying.GetComponent<CharacterController>())
                    {
                        carrying.GetComponent<CharacterController>().enabled = true;
                    }
                    carrying.GetComponent<Collider>().enabled = true;

                    if(!isDragging)
                    {
                        if (carrying.GetComponent<Rigidbody>())
                        {
                            throwDirection.x *= 1.5f;
                            throwDirection.z *= 1.5f;
                            carrying.GetComponent<Rigidbody>().linearVelocity = throwDirection;
                        }
                        else if (carrying.GetComponent<CharacterController>())
                        {
                            carrying.GetComponent<GnomeController>().moveDirection = throwDirection / 3;
                            carrying.GetComponent<GnomeController>().thrown = true;
                        }
                    }
                    carrying = null;
                    isDragging = false;
                }
            }
        }
        else if (!isActive && !controller.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
            controller.Move(moveDirection * speed * Time.deltaTime);
        }

        if (thrown && !isActive)
        {
            moveDirection.y -= gravity / 3 * Time.deltaTime;
            if (controller.enabled) // added this guard
            {
                controller.Move(moveDirection * Time.deltaTime);
            }
            if (controller.isGrounded)
            {
                thrown = false;
            }
        }

        if (carrying)
        {
            if(isDragging)
            {
                carrying.transform.position = Vector3.Lerp(carrying.transform.position, transform.position - transform.forward * 3.5f - transform.up * 0.5f, Time.deltaTime * 15f);
            }
            else
            {
                if (carrying.CompareTag("Gnome"))
                {
                    carrying.transform.position = new Vector3(transform.position.x, transform.position.y + 5f, transform.position.z);
                    carrying.transform.rotation = transform.rotation;
                }
                else if (carrying.layer == LayerMask.NameToLayer("Key"))
                {
                    carrying.transform.position = new Vector3(transform.position.x + 1.5f, transform.position.y + 1.5f, transform.position.z - 1f);
                    carrying.transform.rotation = transform.rotation * Quaternion.Euler(0f, 180f, 0f);
                    dm.story.variablesState["have_key"] = true;
                }
                else
                {
                    carrying.transform.position = new Vector3(transform.position.x, transform.position.y + 4f, transform.position.z);
                    carrying.transform.rotation = transform.rotation;
                }
            }
        }
        bool isMoving = input.magnitude > 0.1f && controller.isGrounded && isActive;
        if (!isMoving)
        {
            runprt.Stop();
        }
        if (animator)
        {
            animator.SetBool("isWalking", isMoving);
        }
        if(carrying && carrying.CompareTag("Throwable") && carrying.gameObject.layer == LayerMask.NameToLayer("Key"))
        {
         //   dm.StartDialogue("get_key");
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
                source.clip = jump;
                source.Play();
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

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Troll"))
        {
            source.clip = death;
            source.Play();
            Time.timeScale = 0;
            deathText.enabled = true;
        }

        if (other.CompareTag("Trigger"))
        {
            dm.StartDialogue("reach_wall");
        }
    }

}
