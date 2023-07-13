using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BallScript : MonoBehaviourGameplay
{
    public float speed = 5f;

    private Vector2 direction;

    public float wallPosition;
    public float ceilingPosition;

    void Start()
    {
        direction = new Vector2(1f, 1f).normalized;
    }

    // Update is called once per frame
    public override void ManagedUpdate()
    {

        transform.Translate(direction * speed * Time.deltaTime);

        if (transform.position.x >= wallPosition || transform.position.x <= -wallPosition)
        {           
            direction.x = -direction.x;
        }

        if (transform.position.y >= ceilingPosition || transform.position.y <= -ceilingPosition)
        {
            direction.y = -direction.y;
        }

    }

    public void ChangeDirecction(Vector2 impactedWall) 
    {
        if (impactedWall == Vector2.up || impactedWall == Vector2.down) 
        {
            direction.y = -direction.y;
        }

        if (impactedWall == Vector2.left || impactedWall == Vector2.right) 
        {
            direction.x = -direction.x;
        }
    }
    
}
