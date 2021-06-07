using UnityEngine;

public class Asteroid : MonoBehaviour
{
    float speed;

    // Start is called before the first frame update
    void Start()
    {
        speed = Random.Range(1, 3);
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector3.right * (speed/2) * Time.deltaTime);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Deadzone")
        {
            Destroy(gameObject);
        }
    }
}
