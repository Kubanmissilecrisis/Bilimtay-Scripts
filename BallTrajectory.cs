using System.Collections;
using UnityEngine.UI;
using System.Collections.Generic;
using UnityEngine;

public class BallTrajectory : MonoBehaviour
{
    [Tooltip("Position we want to hit")]
    public Vector3 targetPos;

    [Tooltip("Horizontal speed, in units/sec")]
    public float speed = 10;

    [Tooltip("How high the arc should be, in units")]
    public float arcHeight = 1;

    //for moving along with the hand
    public GameObject ballReleasePoint;
    bool isGrabbed = true;

    public Animator ballScore;
    public GameObject manager;

    Vector3 startPos;
    private Vector3 initialPosition;

    bool isFlying = false;
    /*
    [SerializeField] private Button button1;
    [SerializeField] private Button button2;
    [SerializeField] private Button button3;
    [SerializeField] private Button button4; */

    public GameObject ring1;
    public GameObject ring2;
    public GameObject ring3;
    public GameObject ring4;
    public GameObject ballSpawnPosition;
    public GameObject ballParentObject;
    public GameObject resetPosMethod;
    public GameObject ballSprite;
      // Reference to the Sheep object
     [SerializeField] GameObject kozuObject;
    [SerializeField] AudioSource ringSound;

    public ParticleSystem confetti1;
    public ParticleSystem confetti2;

    public void RingSound()
    {
        ringSound.Play();
    }

    public void TriggerKozuCheering()
    {
        Animator kozuAnimator = kozuObject.GetComponent<Animator>();
        kozuAnimator.SetTrigger("kozuCheer");
    }

    public void EmitConfetti()
    {
        confetti1.Play();
        confetti2.Play();
    }

    public void FirstButton()
    {
        targetPos = ring1.transform.position;
        //isFlying = true;
       // DisableButtons();
    }

    public void SecondButton()
    {

        targetPos = ring2.transform.position;
        //isFlying = true;
        //DisableButtons();
    }

    public void ThirdButton()
    {
        targetPos = ring3.transform.position;
        // isFlying = true;
       // DisableButtons();
    }

    public void ForthButton()
    {
        targetPos = ring4.transform.position;
        // isFlying = true;
       // DisableButtons();
    }
     public void Destroy()
     {
        // Destroy(gameObject);

        ballSprite.SetActive(false);
        // ballParentObject.transform.localPosition = initialPosition; 

        // transform.localosition = Vector3.zero;
        transform.position = ballParentObject.transform.position;
        isGrabbed = true; 
    }
    /*
     private void DisableButtons()
     {
        button1.interactable = false;
        button2.interactable = false;
        button3.interactable = false;
        button4.interactable = false;
     }
    public void EnableButtons()
    {
        button1.interactable = true;
        button2.interactable = true;
        button3.interactable = true;
        button4.interactable = true;
    }
    /*
    public void Throw()
    {
        bool isGrabbed = false;
    } */
    // called from Kozu script
    public void Fly()
    {
        isGrabbed = false;
        isFlying = true;
    } 

    // call for the generate Question method in QuestionMechanics Script so that new Question
    //can be generated. This method itsel is called from the Animation of the Ball object
     public void CallForGenerateQuestion()
      {
        // Get the QuestionMechanics script from the manager reference
        QuestionMechanics questionMechanics = manager.GetComponent<QuestionMechanics>();

        // Call the GenerateQuestionDelay method on the QuestionMechanics script
        questionMechanics.GenerateQuestionDelay();
       
    }

    void Start()
    {
        // Cache our start position, which is really the only thing we need
        // (in addition to our current position, and the target).
        //startPos = transform.position;

        startPos = ballReleasePoint.transform.position; 


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
            ballScore.SetBool("SpiningOnAir", true);
            // Do something when we reach the target
            // if (nextPos == targetPos) Arrived();
            if (Vector3.Distance(transform.position, targetPos) < 0.1f) Arrived();
        }
         else  if (isGrabbed)
         {

            // move the child object and the Ball object along with the hand during the animation
            transform.position = ballParentObject.transform.position;
         }

    }
       
    
    

    void Arrived()
    {
        // transform.position = new Vector3.y;
        // Destroy(gameObject);
        ballScore.SetBool("SpiningOnAir", false);
        isFlying = false;
        ballScore.SetTrigger("ballScoringFall");
    }

  /*  /// 
    /// This is a 2D version of Quaternion.LookAt; it returns a quaternion
    /// that makes the local +X axis point in the given forward direction.
    /// 
    /// forward direction
    /// Quaternion that rotates +X to align with forward
    static Quaternion LookAt2D(Vector2 forward)
    {
        return Quaternion.Euler(0, 0, Mathf.Atan2(forward.y, forward.x) * Mathf.Rad2Deg);
    }  */

    /* 
     public float time = 3;//represents the time from point A to point B
     public Transform pointA;//Point A
     public Transform pointB;//Point B
     public float g = -10;//gravity acceleration
                          // Use this for initialization
     private Vector3 speed;//initial speed vector
     private Vector3 Gravity;//Gravity vector
     void Start()
     {

         transform.position = pointA.position;//Place the object at point A
                                              //Calculate the initial velocity through an equation
         speed = new Vector3((pointB.position.x - pointA.position.x) / time,
             (pointB.position.y - pointA.position.y) / time - 0.5f * g * time, (pointB.position.z - pointA.position.z) / time);
         Gravity = Vector3.zero;//The initial velocity of gravity is 0
     }
     private float dTime = 0;
     // Update is called once per frame

     void FixedUpdate()
     {

         Gravity.y = g * (dTime += Time.fixedDeltaTime);//v=at
                                                        //Analog displacement
         transform.Translate(speed * Time.fixedDeltaTime);
         transform.Translate(Gravity * Time.fixedDeltaTime);
     } 

     //2
     /*
     public Transform jumpFrom; // starting point
     public Transform jumpTo; // end
     private Vector3 posStart, posEnd;
     public float moveSpeed = 2; // actual speed
     public float moveSpeedFixed = 2; // Moving speed
     public float jumpTime = 1.5f; // start point-total time to end
     private float jumpTimer;
     private bool jumpInit = false;

     void Update()
     {
         if (!jumpInit)
         {
             posStart = jumpFrom.position;
             posEnd = jumpTo.position;
             jumpInit = true;
         }
         Jump();
     }

     void Jump()
     {
         jumpTimer += Time.deltaTime * (moveSpeed / moveSpeedFixed);
         float f1 = jumpTimer / jumpTime;
         float f2 = jumpTimer - jumpTimer * f1; // vertical acceleration
         Vector3 v1 = Vector3.Lerp(posStart, posEnd, f1); // Horizontal uniform motion
         transform.position = v1 + f2 * Vector3.up;
         if (jumpTimer >= jumpTime)
         {
             jumpTimer = 0;
             jumpInit = false;
         }
     }*/


}
