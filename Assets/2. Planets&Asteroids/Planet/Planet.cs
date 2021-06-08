using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Planet : MonoBehaviour
{
    // Start is called before the first frame update

    float life = 1f;

    [SerializeField] Slider lifeSlider;

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Moon")
        {
            DecreaseLife();
        }
    }


    public void DecreaseLife()
    {
        if (life > 0.2)
        {
            life -= 0.1f;
            lifeSlider.value -= 0.1f;
        }
        else
        {
            //TODO Victory
            Time.timeScale = 0;
        }
        
    }
}
