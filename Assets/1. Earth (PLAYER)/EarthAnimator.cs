using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] propuslorIdleSprite;
    [SerializeField] Sprite[] propuslorHitSprite;

    [SerializeField] GameObject propuslor;


    IEnumerator propulsorIdle;
    IEnumerator propulsorHit;


    private void Start()
    {
        propulsorIdle = AnimationController.LoopingCicle(this.propuslor, propuslorIdleSprite, 0.1f);
        StartCoroutine(propulsorIdle);
    }







}
