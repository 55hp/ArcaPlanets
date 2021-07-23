using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed;
    
    [SerializeField] int hp;
    [SerializeField] GameObject mySpriteObj;
    public GameObject powerUp;

    [SerializeField] Color[] asteroidColors;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1, 3);
        mySpriteObj.GetComponent<SpriteRenderer>().color = asteroidColors[Random.Range(0, asteroidColors.Length)];
    }
    
    public void GivePowerUp(GameObject randomPowerUp)
    {
        powerUp = randomPowerUp;
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
                if (powerUp != null)
                {
                    Instantiate(powerUp , gameObject.transform);
                }
                //TODO Aspetta prima un paio di frame 
                Destroy(gameObject);
            }
        }
    }
}
