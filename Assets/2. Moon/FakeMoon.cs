using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FakeMoon : MonoBehaviour
{
    Rigidbody2D myRb;

    [SerializeField] GameObject moonHitPrefab;
    [SerializeField] Sprite[] skins;
    [SerializeField] float constantSpeed;


    private void Start()
    {
        if (constantSpeed <= 0) constantSpeed = 3.5f;
        myRb = gameObject.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<SpriteRenderer>().sprite = Ut.ROA<Sprite>(skins);
    }

    private void LateUpdate()
    {
        myRb.velocity = constantSpeed * (myRb.velocity.normalized);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        Instantiate(moonHitPrefab, new Vector3(collision.GetContact(0).point.x, collision.GetContact(0).point.y, 0), Quaternion.identity);
    }
}
