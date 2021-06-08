using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : Singleton<EarthController>
{
    [SerializeField] Swipe swipeController;
    [SerializeField] float speed;

    private void FixedUpdate()
    {
        if (swipeController.SwipeLeft)
        {
            this.transform.position += Vector3.left * speed * Time.fixedDeltaTime;
        }
        else if (swipeController.SwipeRight)
        {
            this.transform.position += Vector3.right * speed * Time.fixedDeltaTime;
        }
    }

    private void OnCollisionEnter2D(Collision2D coll)
    {
        if (coll.gameObject.tag == "Moon")
        {
            Rigidbody2D ballRb = coll.gameObject.GetComponent<Rigidbody2D>();
            Vector3 hitPoint = coll.contacts[0].point;
            Vector3 paddleCenter = new Vector3(this.gameObject.transform.position.x, this.gameObject.transform.position.y);

            ballRb.velocity = Vector2.zero;

            float difference = paddleCenter.x - hitPoint.x;

            if (hitPoint.x < paddleCenter.x)
            {
                ballRb.AddForce(new Vector2(-(Mathf.Abs(difference * 200)), MoonManager.Instance.initialBallSpeed));
            }
            else
            {
                ballRb.AddForce(new Vector2((Mathf.Abs(difference * 200)), MoonManager.Instance.initialBallSpeed));
            }
        }
    }

}
