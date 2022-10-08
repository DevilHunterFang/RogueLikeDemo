using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    public LayerMask roomMask;

    public float offsetX;
    public float offsetY;
    public GameObject upWay, downWay, leftWay, rightWay;
    public int wayCount;
    public float dist;
    public WallType wallType;

    bool upLinked, downLinked, leftLinked, rightLinked;


    void Start()
    {
        InitializeLink();
        GenerateDoor();
        InitializeWall();
    }

    public void GenerateDoor()
    {
        if (upLinked)
        {
            upWay.SetActive(true);
            wayCount++;
        }
        if (downLinked)
        {
            downWay.SetActive(true);
            wayCount++;
        }
        if (rightLinked)
        {
            rightWay.SetActive(true);
            wayCount++;
        }
        if (leftLinked)
        {
            leftWay.SetActive(true);
            wayCount++;
        }
    }

    private void InitializeLink()
    {
        if (Physics2D.OverlapCircle(new Vector3(this.transform.position.x, this.transform.position.y + offsetY), 0.5f, roomMask))
        {
            upLinked = true;
        }
        if (Physics2D.OverlapCircle(new Vector3(this.transform.position.x, this.transform.position.y - offsetY), 0.5f, roomMask))
        {
            downLinked = true;
        }
        if (Physics2D.OverlapCircle(new Vector3(this.transform.position.x + offsetX, this.transform.position.y), 0.5f, roomMask))
        {
            rightLinked = true;
        }
        if (Physics2D.OverlapCircle(new Vector3(this.transform.position.x - offsetX, this.transform.position.y), 0.5f, roomMask))
        {
            leftLinked = true;
        }
    }

    private void InitializeWall()
    {
        GameObject wall = null;
        switch (wayCount)
        {
            case 1:
                if (upLinked)
                {
                    wall = Instantiate(wallType.UWall, this.transform.position, Quaternion.identity);
                }
                if (downLinked)
                {
                    wall = Instantiate(wallType.DWall, this.transform.position, Quaternion.identity);
                }
                if (leftLinked)
                {
                    wall = Instantiate(wallType.LWall, this.transform.position, Quaternion.identity);
                }
                if (rightLinked)
                {
                    wall = Instantiate(wallType.RWall, this.transform.position, Quaternion.identity);
                }
                break;
            case 2:
                if (upLinked)
                {
                    if (leftLinked)
                    {
                        wall = Instantiate(wallType.LUWall, this.transform.position, Quaternion.identity);
                    }
                    if (rightLinked)
                    {
                        wall = Instantiate(wallType.RUWall, this.transform.position, Quaternion.identity);
                    }
                    if (downLinked)
                    {
                        wall = Instantiate(wallType.UDWall, this.transform.position, Quaternion.identity);
                    }
                }
                if (downLinked)
                {
                    if (leftLinked)
                    {
                        wall = Instantiate(wallType.LDWall, this.transform.position, Quaternion.identity);
                    }
                    if (rightLinked)
                    {
                        wall = Instantiate(wallType.RDWall, this.transform.position, Quaternion.identity);
                    }
                }
                if (leftLinked && rightLinked)
                {
                    wall = Instantiate(wallType.LRWall, this.transform.position, Quaternion.identity);
                }
                break;
            case 3:
                if (leftLinked)
                {
                    if (rightLinked)
                    {
                        if (upLinked)
                        {
                            wall = Instantiate(wallType.LURWall, this.transform.position, Quaternion.identity);
                        }
                        if (downLinked)
                        {
                            wall = Instantiate(wallType.LDRWall, this.transform.position, Quaternion.identity);
                        }
                    }
                    if (upLinked && downLinked)
                    {
                        wall = Instantiate(wallType.LUDWall, this.transform.position, Quaternion.identity);
                    }
                }
                if (rightLinked)
                {
                    if (upLinked && downLinked)
                    {
                        wall = Instantiate(wallType.RUDWall, this.transform.position, Quaternion.identity);
                    }
                }
                break;
            case 4:
                wall = Instantiate(wallType.LURDWall, this.transform.position, Quaternion.identity);
                break;
        }
        if (wall != null)
        {
            wall.transform.SetParent(this.transform);
            wall.transform.position = new Vector3(wall.transform.position.x + 8f, wall.transform.position.y - 3f, wall.transform.position.z);
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            CameraController.instance.SetTarget(this.transform);
        }
    }

    [System.Serializable]
    public class WallType
    {
        public GameObject LWall, RWall, UWall, DWall,
                   LRWall, LUWall, LDWall, RUWall, RDWall, UDWall,
                   LURWall, LDRWall, RUDWall, LUDWall,
                   LURDWall;
    }
}
