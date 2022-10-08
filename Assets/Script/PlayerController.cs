using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float speed;

    Rigidbody2D rb;
    Animator animator;

    Vector2 force;

    void Start()
    {
        rb = this.GetComponent<Rigidbody2D>();
        animator = this.GetComponent<Animator>();
    }

    void Update()
    {
        force.x = Input.GetAxisRaw("Horizontal");
        force.y = Input.GetAxisRaw("Vertical");
        if(force.x > 0){
            this.transform.localScale = new Vector3(1, 1, 1);
        }else{
            this.transform.localScale = new Vector3(-1, 1, 1);
        }
        animator.SetFloat("Speed", force.magnitude);
    }

    private void FixedUpdate() {
        rb.MovePosition(rb.position + force * speed * Time.fixedDeltaTime);
    }
}
