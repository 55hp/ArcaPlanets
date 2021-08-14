using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : MonoBehaviour 
{
    /*
    [SerializeField] GameObject shield;
    [SerializeField] GameObject myFace;

    IEnumerator activeShieldPowerType;

    float startingTime;
    float activeTime;
    float inactiveTime;

    Sprite[] blinkShieldIntro;
    Sprite[] blinkShieldLoop;
    Sprite[] blinkShieldEnd;

    Sprite[] shieldCreationFacesAnimation;

    public void OnEnable()
    {
        shield.SetActive(true);
        if (activeShieldPowerType != null) StartCoroutine(activeShieldPowerType);
    }

    public void OnDisable()
    {
        if (activeShieldPowerType != null) StopCoroutine(activeShieldPowerType);
        shield.SetActive(false);
    }
    
    public void SetBlinkingShield(float startingTime , float activeTime , float inactiveTime, Sprite[] blinkShieldIntro , Sprite[] blinkShieldLoop , Sprite[] blinkShieldEnd , Sprite[] shieldCreationFacesAnimation)
    {
        this.startingTime = startingTime;
        this. activeTime = activeTime;
        this. inactiveTime = inactiveTime;
        this.blinkShieldIntro = blinkShieldIntro;
        this.blinkShieldLoop = blinkShieldLoop;
        this.blinkShieldEnd = blinkShieldEnd;
        this.shieldCreationFacesAnimation = shieldCreationFacesAnimation;

        activeShieldPowerType = BlinkShield();
    }

    IEnumerator BlinkShield()
    {
        shield.SetActive(false);
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            gameObject.GetComponent<MobAnimationController>().StopIdle();
            Coroutine faceFirst = StartCoroutine(AnimationController.FixedCicle(myFace, shieldCreationFacesAnimation, 0.12f));

            yield return faceFirst;

            Coroutine a = StartCoroutine(AnimationController.FixedCicle(shield, blinkShieldIntro, 0.08f));
            shield.SetActive(true);

            yield return a;

            gameObject.GetComponent<MobAnimationController>().StartIdle();
            Coroutine b = StartCoroutine(AnimationController.LoopingCicle(shield, blinkShieldLoop, 0.12f));

            yield return new WaitForSeconds(activeTime);

            StopCoroutine(b);

            Coroutine c = StartCoroutine(AnimationController.FixedCicle(shield, blinkShieldEnd, 0.1f));

            yield return c;
            shield.SetActive(false);
            yield return new WaitForSeconds(inactiveTime);
        }
    }
    */
}
