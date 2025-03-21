using UnityEngine;

public class Door : MonoBehaviour
{
    [SerializeField] public Transform previousRoom;
    [SerializeField] public Transform nextRoom;
    [SerializeField] public CameraController cam;

    private void Awake()
    {
        cam = Camera.main.GetComponent<CameraController>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            if (collision.transform.position.x < transform.position.x)
            {
                // (next room)
                cam.MoveToNewRoom(nextRoom);
                nextRoom.GetComponent<Room>().ActivateRoom(true);
                previousRoom.GetComponent<Room>().ActivateRoom(false);
            }
            else
            {
                // (previous room)
                cam.MoveToNewRoom(previousRoom);
                previousRoom.GetComponent<Room>().ActivateRoom(true);
                nextRoom.GetComponent<Room>().ActivateRoom(false);
            }
        }
    }

    public void setNextRoom(Transform nextRoom)
    {
        this.nextRoom = nextRoom;
    }

    public void setPreviousRoom(Transform previousRoom)
    {
        this.previousRoom = previousRoom;
    }

}
