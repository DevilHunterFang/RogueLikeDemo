using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMap : MonoBehaviour
{
    GameObject map;
    void OnEnable()
    {
        map = this.transform.parent.GetChild(0).gameObject;
        map.SetActive(false);
    }

    private void OnTriggerEnter2D(Collider2D other) {
        if(other.CompareTag("Player")){
            map.SetActive(true);
        }
    }
}
