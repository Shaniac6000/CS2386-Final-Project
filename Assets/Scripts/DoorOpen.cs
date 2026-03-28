using UnityEngine;

public class DoorOpen : MonoBehaviour
{
    public Transform door1;
    public Transform door2;
    public LockBehavior lb;
    public PressurePlate p1;
    public PressurePlate p2;

    // Update is called once per frame
    void Update()
    {
        if (!lb.locked && p1.isPressed && p2.isPressed)
        {
            door1.rotation = Quaternion.Lerp(door1.rotation, Quaternion.Euler(0,90,0), .5f * Time.deltaTime);
            door2.rotation = Quaternion.Lerp(door2.rotation, Quaternion.Euler(0,-90,0), .5f * Time.deltaTime);
        }
    }
}
