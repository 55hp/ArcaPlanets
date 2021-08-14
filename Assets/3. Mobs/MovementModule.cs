using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovementModule : MonoBehaviour 
{
    Vector3 startingPosition;
    Color startingColor;

    float movementSpeed;
    Vector3 dir;
    float distance;

    IEnumerator movement;

    public enum MovSpeed
    {
        Slow,
        Medium,
        Fast 
    }

    private void OnEnable()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;
        startingPosition = this.gameObject.transform.position;
        if(movement != null)
        {
            StartCoroutine(movement);
        }
    }

    private void OnDisable()
    {
        gameObject.GetComponent<SpriteRenderer>().color = startingColor;
        gameObject.transform.position = startingPosition;
        if (movement != null)
        {
            StopCoroutine(movement);
        }
    }


    public void GiveMeHorizontalMovement(float maxDistance, MovSpeed speed)
    {
        movement = IdleLeftRight();
        switch (speed)
        {
            case MovSpeed.Slow:
                movementSpeed = 0.2f;
                break;
            case MovSpeed.Medium:
                movementSpeed = 0.4f;
                break;
            case MovSpeed.Fast:
                movementSpeed = 0.6f;
                break;
        }
        distance = maxDistance;
    }

    public void GiveMeVerticalMovement(float maxDistance, MovSpeed speed)
    {
        movement = IdleUpDown();
        switch (speed)
        {
            case MovSpeed.Slow:
                movementSpeed = 0.2f;
                break;
            case MovSpeed.Medium:
                movementSpeed = 0.4f;
                break;
            case MovSpeed.Fast:
                movementSpeed = 0.6f;
                break;
        }
        distance = maxDistance;
    }


    IEnumerator IdleLeftRight()
    {
        dir = Vector3.right;
        bool comeBack = false;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (!comeBack)
            {
                transform.position += dir * movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= dir * movementSpeed * Time.deltaTime;
            }

            if (transform.position.x > startingPosition.x + distance)
            {
                comeBack = true;
            }

            if (transform.position.x < startingPosition.x - distance)
            {
                comeBack = false;
            }

        }
    }

    IEnumerator IdleUpDown()
    {
        dir = Vector3.up;
        bool comeBack = false;
        while (true)
        {
            yield return new WaitForEndOfFrame();
            if (!comeBack)
            {
                transform.position += dir * movementSpeed * Time.deltaTime;
            }
            else
            {
                transform.position -= dir * movementSpeed * Time.deltaTime;
            }

            if (transform.position.y > startingPosition.y + distance)
            {
                comeBack = true;
            }

            if (transform.position.y < startingPosition.y - distance)
            {
                comeBack = false;
            }

        }
    }
}
