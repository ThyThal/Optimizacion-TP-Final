using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CustomBoxCollider : MonoBehaviourGameplay
{
    public GameObject ball;
    private BallScript ballScript;
    public Transform brick;
    public float ballRadius = 0.5f;
    public float tolerance = 0.5f;
    public float maxDistance = 1f;
    public Vector2 impactNormal;

    private void Start()
    {
        ball = GameObject.FindGameObjectWithTag("Ball");
        ballScript = ball.GetComponent<BallScript>();
    }
    public override void ManagedUpdate()
    {
        if (IsBallTouchingBrick(ball.transform.position, brick.position, brick.localScale, maxDistance))
        {
            ballScript.ChangeDirecction(impactNormal);
            gameObject.SetActive(false);
        }
    }

    private bool IsBallTouchingBrick(Vector3 ballPosition, Vector3 brickPosition, Vector3 brickScale, float maxDistance)
    {
        float halfWidth = brickScale.x / 2f;
        float halfHeight = brickScale.y / 2f;

        float leftEdge = brickPosition.x - halfWidth - ballRadius;
        float rightEdge = brickPosition.x + halfWidth + ballRadius;
        float topEdge = brickPosition.y + halfHeight + ballRadius;
        float bottomEdge = brickPosition.y - halfHeight - ballRadius;

        bool isWithinHorizontalRange = ballPosition.x >= (leftEdge - tolerance) && ballPosition.x <= (rightEdge + tolerance);
        bool isWithinVerticalRange = ballPosition.y >= (bottomEdge - tolerance) && ballPosition.y <= (topEdge + tolerance);
        bool isWithinMaxDistance = Vector3.Distance(ballPosition, brickPosition) <= maxDistance;

        impactNormal = Vector2.zero;

        if (isWithinHorizontalRange && isWithinVerticalRange && isWithinMaxDistance)
        {
            if (Mathf.Abs(ballPosition.x - leftEdge) <= tolerance)
            {
                if (Mathf.Abs(ballPosition.y - topEdge) <= tolerance)
                {
                    impactNormal = new Vector2(-1f, 1f);
                }
                else if (Mathf.Abs(ballPosition.y - bottomEdge) <= tolerance)
                {
                    impactNormal = new Vector2(-1f, -1f);
                }
                else
                {
                    impactNormal = Vector2.left;
                }
            }
            else if (Mathf.Abs(ballPosition.x - rightEdge) <= tolerance)
            {
                if (Mathf.Abs(ballPosition.y - topEdge) <= tolerance)
                {
                    impactNormal = new Vector2(1f, 1f);
                }
                else if (Mathf.Abs(ballPosition.y - bottomEdge) <= tolerance)
                {
                    impactNormal = new Vector2(1f, -1f);
                }
                else
                {
                    impactNormal = Vector2.right;
                }
            }
            else if (Mathf.Abs(ballPosition.y - topEdge) <= tolerance)
            {
                impactNormal = Vector2.up;
            }
            else if (Mathf.Abs(ballPosition.y - bottomEdge) <= tolerance)
            {
                impactNormal = Vector2.down;
            }

            return true;
        }

        return false;
    }
}
