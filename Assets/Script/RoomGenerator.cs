using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RoomGenerator : MonoBehaviour
{
    public enum Direction { up, down, left, right }
    public Direction direction;

    [Header("房间信息")]
    public Room roomPrefab;
    public int roomSize;
    public Color startColor, endColor;

    [Header("位置控制")]
    public Transform startPoint;
    public float offsetX;
    public float offsetY;
    public LayerMask roomMask;

    public List<Room> rooms = new List<Room>();


    void Start()
    {
        for (int i = 0; i < roomSize; i++)
        {
            rooms.Add(Instantiate(roomPrefab, startPoint.position, Quaternion.identity));
            ChangePointPos();
        }

        rooms[0].GetComponent<SpriteRenderer>().color = startColor;
        CalculateDistance(roomSize);
        // List<int> dist = Dijkstra(roomSize);
        float max = 0f;
        int index = 0;
        for(int i = 0; i < roomSize; i++){
            if(rooms[i].dist > max){
                max = rooms[i].dist;
                index = i;
            }
        }
        for(int i = 0; i < roomSize; i++){
            if(rooms[i].wayCount == 1 && (rooms[i].dist + 1.0f == max || rooms[i].dist == max)){
                index = i;
            }
        }

        Room endRoom = rooms[index];
        endRoom.GetComponent<SpriteRenderer>().color = endColor;
    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.anyKeyDown)
        // {
        //     SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        // }
    }

    public void ChangePointPos()
    {
        do
        {
            direction = (Direction)Random.Range(0, 4);

            switch (direction)
            {
                case Direction.up:
                    startPoint.position += new Vector3(0, offsetY, 0);
                    break;
                case Direction.down:
                    startPoint.position += new Vector3(0, -offsetY, 0);
                    break;
                case Direction.left:
                    startPoint.position += new Vector3(-offsetX, 0, 0);
                    break;
                case Direction.right:
                    startPoint.position += new Vector3(offsetX, 0, 0);
                    break;
            }
        } while (
            Physics2D.OverlapCircle(startPoint.position, 0.5f, roomMask)
        );
    }

    private void CalculateDistance(int size){
        List<int> distList = new List<int>();
        for(int i = 0; i < size; i++){
            rooms[i].dist = Mathf.Abs(rooms[i].transform.position.x / offsetX) + Mathf.Abs(rooms[i].transform.position.y / offsetY);
        }
    }

    // List<int> Dijkstra(int size)
    // {
    //     List<List<int>> graph = new List<List<int>>();
    //     for (int i = 0; i < size; i++)
    //     {
    //         List<int> list = new List<int>();
    //         Room point = rooms[i];
    //         foreach (Room room in rooms)
    //         {
    //             if (Mathf.Abs(room.transform.position.x - point.transform.position.x) == offsetX
    //             || Mathf.Abs(room.transform.position.y - point.transform.position.y) == offsetY)
    //             {
    //                 list.Add(1);
    //             }
    //             else
    //             {
    //                 list.Add(0);
    //             }
    //         }
    //         graph.Add(list);
    //     }
    //     List<int> visit = new List<int>();
    //     for (int i = 0; i < size; i++)
    //     {
    //         visit.Add(0);
    //     }
    //     List<int> dist = new List<int>();
    //     for (int i = 0; i < size; i++)
    //     {
    //         dist.Add(graph[0][i]);
    //     }
    //     visit[0] = 1;
    //     for(int i = 0; i < size; i++){
    //         int max = 0;
    //         int middle = 0;
    //          for(int j = 0; j < size; j++){
    //              if(visit[j] == 0 && max < dist[j]){
    //                  max = dist[j];
    //                  middle = j;
    //              }
    //          }
    //           for(int j = 0; j < size; j++){
    //               int middleDis = dist[middle] + graph[middle][j];
    //               if(visit[j] == 0 && dist[j] <= middleDis){
    //                   dist[j] = middleDis;
    //               }
    //           }
    //           visit[middle] = 1;
    //     }

    //     return dist;
    // }
}
