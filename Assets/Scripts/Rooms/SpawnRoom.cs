using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EndlessRunner : MonoBehaviour
{
    public GameObject[] roomPrefabs;  // Các prefab của room
    public int initialRooms = 5;      // Số room khởi tạo ban đầu
    public float roomLength = 18f;
    public GameObject firstRoomPrefab; // Chiều dài mỗi room

    private List<GameObject> activeRooms = new List<GameObject>();
    private Transform player;
    private float spawnZ = 0f;
    private float safeZone = 30f;
    private int spawned = 0;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;

        // Tạo room đầu tiên được chỉ định
        GameObject firstRoom = Instantiate(firstRoomPrefab, new Vector3(0, 0, spawnZ), Quaternion.identity);
        firstRoom.SetActive(true);
        firstRoom.transform.Find("Grid").Find("WallFirstRoom")?.gameObject.SetActive(true);
        activeRooms.Add(firstRoom);
        spawned++;
        spawnZ += roomLength;

        // Tạo các room còn lại
        for (int i = 1; i < initialRooms; i++)
        {
            SpawnRoom();
        }
    }

    void Update()
    {
        //if (activeRooms.Count > 0)
        //{
        //    float currentRoomCenter = activeRooms[0].transform.position.x + (roomLength / 2);
        //    if (player.position.x > currentRoomCenter)
        //    {
        //        SpawnRoom();
        //        AddBarrier();
        //    }
        //}
        if (activeRooms.Count > 0 && activeRooms.Count < initialRooms)
        {
            SpawnRoom();
            AddBarrier();
        }
        else
        {
            float center = activeRooms.Last().transform.position.x - roomLength;
            if (player.transform.position.x > center)
            {
                SpawnRoom();
                AddBarrier();
                RemoveRoom();
            }
        }
    }

    private void AddBarrier()
    {
        if (activeRooms[0] != null)
        {
            activeRooms[0].transform.Find("Grid").Find("WallFirstRoom")?.gameObject.SetActive(true);
        }
    }

    void SpawnRoom()
    {
        GameObject room = Instantiate(roomPrefabs[Random.Range(0, roomPrefabs.Length)], new Vector3(spawned * 18, 0, spawnZ), Quaternion.identity);
        room.SetActive(true);
        room.transform.name = $"Room {spawned}";
        activeRooms.Add(room);

        if (activeRooms.Count > 1)
        {
            var thisRoomIndex = activeRooms.Count - 1;
            var beforeRoom = activeRooms[thisRoomIndex - 1];
            room.GetComponentInChildren<Door>()?.setPreviousRoom(beforeRoom.transform);
            room.transform.Find("Grid").Find("WallFirstRoom")?.gameObject.SetActive(false);
            beforeRoom.GetComponentInChildren<Door>()?.setNextRoom(room.transform);
        }

        spawned++;
        spawnZ += roomLength;
    }


    void RemoveRoom()
    {
        Destroy(activeRooms[0]);
        activeRooms.RemoveAt(0);
    }
}
