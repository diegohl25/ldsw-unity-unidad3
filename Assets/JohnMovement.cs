using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JohnMovement : MonoBehaviour
{
    public float jumpForce;
    public float speed;
    public GameObject bulletPrefab;

    private Rigidbody2D rigidbody2D;
    private Animator animator;
    private SpriteRenderer srenderer;

    private float horizontal;
    private bool grounded;
    private float lastShot;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        srenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if(horizontal < 0f)
            srenderer.flipX = true;
        else if(horizontal >0f)
            srenderer.flipX = false;
        
        animator.SetBool("running", horizontal != 0f);

        Debug.DrawRay(transform.position, Vector3.down * 0.1f, Color.red);
        if(Physics2D.Raycast(transform.position, Vector3.down, 0.1f))
            grounded = true;
        else
            grounded = false;

        if(Input.GetKeyDown(KeyCode.W) && grounded){
            Jump();
        }

        if(Input.GetKey(KeyCode.Space) && Time.time > lastShot + 0.25f){
            Shoot();
            lastShot = Time.time;
        }
    }

    private void Jump(){
        rigidbody2D.AddForce(Vector2.up *jumpForce);
    }

    private void FixedUpdate(){
        rigidbody2D.velocity = new Vector2(horizontal *speed, rigidbody2D.velocity.y);
    }

    private void Shoot(){
        GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);
        float dir;
        if(srenderer.flipX == true)
            dir = -1;
        else
            dir = 1;

        bullet.GetComponent<BulletScript>().speed *= dir;
        //bulletPrefab.GetComponent<BulletScript>().Shoot();
    }
}
