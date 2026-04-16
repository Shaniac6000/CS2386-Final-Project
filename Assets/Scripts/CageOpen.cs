using UnityEngine;

public class CageOpen : MonoBehaviour
{
    private bool unlocked;

    // Update is called once per frame
    void Update()
    {
        if (unlocked)
            transform.parent.transform.localRotation = Quaternion.Lerp(transform.parent.transform.localRotation, Quaternion.Euler(0, -90, 0), .5f * Time.deltaTime);
    }

    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Throwable"))
        {
            GetComponent<AudioSource>().Play();
            unlocked = true;
            Destroy(collision.gameObject);
        }
    }
}
