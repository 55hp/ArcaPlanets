using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthAnimator : MonoBehaviour
{
    [SerializeField] Sprite[] propuslorIdleSprite;
    [SerializeField] Sprite[] propuslorHitSprite;

    [SerializeField] GameObject propuslor;
    [SerializeField] ParticleSystem smoke;


    IEnumerator propulsorIdle;
    IEnumerator propulsorHit;


    private void Start()
    {
        propulsorIdle = AnimationController.LoopingCicle(this.propuslor, propuslorIdleSprite, 0.1f);
        propulsorHit = AnimationController.FixedCicle(this.propuslor, propuslorHitSprite, 0.1f);

        StartCoroutine(propulsorIdle);
    }

    public void HitAnimation()
    {
        StartCoroutine(propulsorHit);
        StartCoroutine(StopSmoke());
    }

    IEnumerator StopSmoke()
    {
        smoke.Stop();
        yield return new WaitForSeconds(0.3f);
        smoke.Play();
    }



}
