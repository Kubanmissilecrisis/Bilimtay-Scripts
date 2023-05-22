using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChildBall : MonoBehaviour
{
    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;
    Vector3 startPos;
    public GameObject ballReleasePoint;
    bool isFlying = false;
    private Vector3 initialPosition;
    public GameObject ballParentObject;

    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;
    public GameObject ring4;


    public void FirstButton()
    {
        targetPos = ring1.transform.position;
        //isFlying = true;
        // DisableButtons();
        Fly();
    }

    public void SecondButton()
    {

        targetPos = ring2.transform.position;
        //isFlying = true;
        //DisableButtons();
        Fly();
    }

    public void ThirdButton()
    {
        targetPos = ring3.transform.position;
        // isFlying = true;
        // DisableButtons();
        Fly();
    }

    public void ForthButton()
    {
        targetPos = ring4.transform.position;
        // isFlying = true;
        // DisableButtons();
        Fly();
    }

     void Fly()
    {
        isFlying = true;
    }


    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        //startPos = transform.position;

        startPos = transform.position;


        initialPosition = ballParentObject.transform.localPosition;
    }

    void FixedUpdate()
    {


        if (isFlying)
        {

            // Compute the next position, with arc added in
            float x0 = startPos.x;
            float x1 = targetPos.x;
            float dist = x1 - x0;
            float nextX = Mathf.MoveTowards(transform.position.x, x1, speed * Time.deltaTime);
            float baseY = Mathf.Lerp(startPos.y, targetPos.y, (nextX - x0) / dist);
            float arc = arcHeight * (nextX - x0) * (nextX - x1) / (-0.25f * dist * dist);
            Vector3 nextPos = new Vector3(nextX, baseY + arc, transform.position.z);

            // Rotate to face the next position, and then move there
            // transform.rotation = LookAt2D(nextPos - transform.position);
            transform.position = nextPos;
            //Tringers the Ball spinning animation
          
            // Do something when we reach the target
            // if (nextPos == targetPos) Arrived();
            if (Vector3.Distance(transform.position, targetPos) < 0.1f) Arrived();
        }

    }


    void Arrived()
    {
        // transform.position = new Vector3.y;
        // Destroy(gameObject);

        isFlying = false;

    }


}



