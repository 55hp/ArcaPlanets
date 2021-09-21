using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VFX : MonoBehaviour
{

    [SerializeField] Sprite[] sprites;
    [SerializeField] float frequency;

    private void Start()
    {
        StartCoroutine(AnimationController.FixedCicle(this.gameObject, sprites, frequency));
        StartCoroutine(DestroySync(sprites.Length * frequency));
    }
    
    private IEnumerator DestroySync(float time)
    {
        yield return new WaitForSeconds(time);
        Destroy(gameObject);
    }


}
