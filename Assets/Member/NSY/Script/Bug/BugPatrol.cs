using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BugPatrol : MonoBehaviour
{
    public float speed;
    public float startWaitTime;
    private float waitTime;
    


    public Transform[] moveSpots;
    private int randomSpot;

    private void Start()
    {
        waitTime = startWaitTime;
        randomSpot = Random.Range(0, moveSpots.Length);
      //  render = GetComponentInChildren<SpriteRenderer>();
    }

    public SpriteRenderer render;

    private void FixedUpdate()
    {
        transform.position = Vector3.MoveTowards(transform.position, moveSpots[randomSpot].position, speed * Time.deltaTime);

        if (Vector3.Distance(transform.position, moveSpots[randomSpot].position) < 0.3f)           
        {
            if (waitTime <= 0)
            {
                randomSpot = Random.Range(0, moveSpots.Length);
                waitTime = startWaitTime;
            }
            else
            {
                waitTime -= Time.deltaTime;
            }
        }
        FlipSprite();
    }

    public void FlipSprite()
    {
        // Get the X Value of the patrolPoint
        float pointXValue = moveSpots[randomSpot].transform.position.x;

        // Get the X Value of the Enemy
        float enemyXValue = gameObject.transform.position.x;

    // PatrolPoint is to the Left
        if (pointXValue < enemyXValue)
        {
            // Flip Sprite on the X Axis
            render.flipX = false;
            transform.localScale = new Vector3(-1f, 1f, 1f);
        }
        // PatrolPoint is to the Right
        else if (pointXValue > enemyXValue)
        {
            render.flipX = true ;
            // Flip Sprite back on the X Axis
            transform.localScale = new Vector3(1f, 1f, 1f);
        }
    }
}
