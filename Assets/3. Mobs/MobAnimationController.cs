using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobAnimationController : MonoBehaviour
{

    bool alive;
    Vector3 startingPosition;
    Color startingColor;

    float movementSpeed;
    Vector3 dir;
    float distance;


    public enum MovSpeed
    {
        Slow =1,
        Medium =2,
        Fast =3
    }

    private void OnEnable()
    {
        startingColor = gameObject.GetComponent<SpriteRenderer>().color;
        startingPosition = this.gameObject.transform.position;
        alive = this.GetComponent<Mob>().ImAlive();
    }

    private void OnDisable()
    {
        gameObject.GetComponent<SpriteRenderer>().color = startingColor;
        gameObject.transform.position = startingPosition;
        StopAllCoroutines();
        alive = false;
    }

    public void MakeMeMoving(bool upDown , float maxDistance , MovSpeed speed)
    {
        if (!upDown && maxDistance > 0 && alive)
        {
            StartCoroutine(IdleLeftRight());
        }
        else if (upDown && maxDistance > 0 && alive)
        {
            StartCoroutine(IdleUpDown());
        }

        switch ((int)speed)
        {
            case 1:
                movementSpeed = 0.2f;
                break;
            case 2:
                movementSpeed = 0.4f;
                break;
            case 3:
                movementSpeed = 0.6f;
                break;
        }

        distance = maxDistance;
    }
    
        

    IEnumerator IdleLeftRight()
    {
        dir = Vector3.right;
        bool comeBack = false;
        while (alive)
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
        while (alive)
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

    public void HitAnimation()
    {
        StartCoroutine(Ouch());
    }

    IEnumerator Ouch()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Color.black;
        yield return new WaitForSeconds(0.1f);
        gameObject.GetComponent<SpriteRenderer>().color = startingColor;
    }

}
