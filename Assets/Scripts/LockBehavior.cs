using UnityEngine;

public class LockBehavior : MonoBehaviour
{
    public bool locked = true;
    public AudioClip sfx;
    private Rigidbody rb;
    public ParticleSystem prt;

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
            prt.Play();
            if (sfx)
                AudioSource.PlayClipAtPoint(sfx, transform.position);

            var dm = FindFirstObjectByType<DialogueManager>();
            dm.story.variablesState["lock_down"] = true;

            TrappedGnome tg = FindFirstObjectByType<TrappedGnome>();
    
            if(!tg.trapped)
            {
                dm.StartDialogue("after_get_up");
            }
        }
    }
}
