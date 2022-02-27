using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrabController : MonoBehaviour
{
    public Transform grabDetect;
    public Transform grabber;
    public float rayDist;
    SpriteRenderer spriteFlip;

    private void Start()
    {
        spriteFlip = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Grab();
        Throw();
    }

    private void Grab()
    {
        RaycastHit2D grabCheck = Physics2D.Raycast(grabDetect.position, Vector2.right * transform.localScale, rayDist);

        if (grabCheck.collider != null && grabCheck.collider.tag == "Grabbable")
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                grabCheck.collider.gameObject.transform.parent = grabber;
                grabCheck.collider.gameObject.transform.position = grabber.position;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = true;
            }
            else
            {
                grabCheck.collider.gameObject.transform.parent = null;
                grabCheck.collider.gameObject.GetComponent<Rigidbody2D>().isKinematic = false;
            }
        }
    }

    private void Throw()
    {
        // detect whether something is being grabbed
        // if left and right click were pressed 
        // disable grab
        // move the object that was grabbed
        
    }
}
