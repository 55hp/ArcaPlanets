using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    [SerializeField] float frequency;
    [SerializeField] float startingTime;

    private void Start()
    {
        if(startingTime != 0)
        {
            StartCoroutine(AnimationController.FixedCicle(this.gameObject, sprites, frequency , startingTime));
        }
        else
        {
            StartCoroutine(AnimationController.FixedCicle(this.gameObject, sprites, frequency));
        }
        StartCoroutine(DestroySync(sprites.Length * frequency));
    }
    
    private IEnumerator DestroySync( float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
