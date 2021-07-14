using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation;

public class PowerUp : MonoBehaviour
{
    [SerializeField] int id;
    [SerializeField] float timerEffect;

    PathCreator pathCreator;
    public EndOfPathInstruction endOfPathInstruction;
    public float speed = 5;
    float distanceTravelled;


    void Update()
    {
        if (pathCreator != null)
        {
            distanceTravelled += speed * Time.deltaTime;
            transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
        }
    }

    public void SetPathCreator(PathCreator pathCreator)
    {
        this.pathCreator = pathCreator;
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Earth"))
        {
            PowerUpManager.Instance.TriggerPowerUpEffect(id, timerEffect);
            Destroy(gameObject);
        }
    }
}

