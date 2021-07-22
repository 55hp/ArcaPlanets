using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed;
    int hp;
    [SerializeField] GameObject mySpriteObj;


    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1, 3);
    }


    public Asteroid(Sprite mySprite , int hp)
    {
        this.hp = hp;
        mySpriteObj.GetComponent<SpriteRenderer>().sprite = mySprite;
    }


    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (speed/2) * Time.deltaTime);
        mySpriteObj.transform.Rotate(Vector3.forward * 0.2f,Space.Self);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Deadzone"))
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Moon"))
        {
            hp--;

        if(hp <= 0)
            {
                Destroy(gameObject);
            }
        }
    }
}
