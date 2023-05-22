using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KozuMechanics : MonoBehaviour
{
    public GameObject ballInitialPos;
    [SerializeField] Animator ballAnimations;
    public GameObject ballObject;
    /*
    public void CallForTheBallScript()
    {
        // Get the BallTrajectory script from the ballObject reference
        BallTrajectory ballTrajectory = ballObject.GetComponent<BallTrajectory>();

        // Call the Throw method on the BallTrajectory script
        ballTrajectory.Throw();

    } */

    public void CallForFlyMethod()
    {
        // Get the BallTrajectory script from the ballObject reference
        BallTrajectory ballTrajectory = ballObject.GetComponent<BallTrajectory>();

        // Call the Throw method on the BallTrajectory script
        ballTrajectory.Fly();

    }

    public void CallForBallFailedAnim()
    {
        ballAnimations.SetTrigger("IsFailedThrow");
    }

}
