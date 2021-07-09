using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwipeInput : MonoBehaviour
{

    private Vector2 fingerDown;
    private Vector2 fingerUp;
    public bool detectSwipeOnlyAfterRelease = false;

    public float SWIPE_THRESHOLD = 20f;

    public float force;
    private Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();

    }
    void FixedUpdate()
    {

        foreach (Touch touch in Input.touches)
        {
            if (touch.phase == TouchPhase.Began)
            {
                fingerUp = touch.position;
                fingerDown = touch.position;
            }

            //Detects Swipe while finger is still moving
            if (touch.phase == TouchPhase.Moved)
            {
                if (!detectSwipeOnlyAfterRelease)
                {
                    fingerDown = touch.position;
                    CheckSwipe();
                }
            }

            //Detects swipe after finger is released
            if (touch.phase == TouchPhase.Ended)
            {
                fingerDown = touch.position;
                CheckSwipe();
            }
        }
    }

    void CheckSwipe()
    {
        //Check if Horizontal swipe
        if (HorizontalMove() > SWIPE_THRESHOLD && HorizontalMove() > VerticalMove())
        {
            //Debug.Log("Horizontal");
            if (fingerDown.x - fingerUp.x > 0)//Right swipe
            {
                OnSwipeRight();
            }
            else if (fingerDown.x - fingerUp.x < 0)//Left swipe
            {
                OnSwipeLeft();
            }
            fingerUp = fingerDown;
        }

        //No Movement
        else
        {
            //Debug.Log("No Swipe!");
        }
    }

    float VerticalMove()
    {
        return Mathf.Abs(fingerDown.y - fingerUp.y);
    }

    float HorizontalMove()
    {
        return Mathf.Abs(fingerDown.x - fingerUp.x);
    }

    
    void OnSwipeLeft()
    {
        //Debug.Log("Swipe Left");
        rb.AddRelativeTorque(Vector3.forward * force * Time.deltaTime);
    }

    void OnSwipeRight()
    {
        //Debug.Log("Swipe Right");
        rb.AddRelativeTorque(Vector3.forward * -force * Time.deltaTime);
    }
}