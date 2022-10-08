using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    public static CameraController instance;
    public float speed;
    private Transform target;

    private void Awake() {
        if(instance !=null){
            Destroy(this);
            return;
        }
        instance = this;
        DontDestroyOnLoad(this);
    }

    // Update is called once per frame
    void Update()
    {
        if(target != null){
            this.transform.position = Vector3.MoveTowards(this.transform.position, new Vector3(target.position.x, target.position.y, this.transform.position.z), speed);
        }
    }

    public void SetTarget(Transform newTarget){
        target = newTarget;
    }
}
