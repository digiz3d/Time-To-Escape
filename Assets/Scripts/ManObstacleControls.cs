using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ManObstacleControls : MonoBehaviour
{
    private LeandrePlayerControl playerControl;
    private float beforeSlowRunningSpeed;
    private float beforeSlowSlidingSpeed;
    private bool firstTriggerPassed;
    private float hello;
    private float tom;

    // Start is called before the first frame update
    void Start()
    {
        firstTriggerPassed = false;
    }

    private void OnTriggerEnter2D(Collider2D player)
    {
        if(player.tag == "Player")
        {
            playerControl = player.GetComponent<LeandrePlayerControl>();

            if(firstTriggerPassed == false)
            {
                beforeSlowRunningSpeed = (float)playerControl.getRunningSpeed();
                beforeSlowSlidingSpeed = (float)playerControl.getSlidingSpeed();
                firstTriggerPassed = true;
            }

            playerControl.setRunningSpeed(playerControl.getRunningSpeed() * 0.1f);
            playerControl.setSlidingSpeed(playerControl.getSlidingSpeed() * 0.1f);
        }
    }

    private void OnTriggerExit2D(Collider2D player)
    {
        if (player.tag == "Player")
        {
            playerControl = player.GetComponent<LeandrePlayerControl>();
            playerControl.setRunningSpeed(beforeSlowRunningSpeed);
            playerControl.setSlidingSpeed(beforeSlowSlidingSpeed);
        }
    }
}
