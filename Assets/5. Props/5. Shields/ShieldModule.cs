using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : MonoBehaviour
{
    [SerializeField] Sprite[] blinkShieldIntro;
    [SerializeField] Sprite[] blinkShieldLoop;
    [SerializeField] Sprite[] blinkShieldEnd;

    [SerializeField] GameObject shield;

    float startingTime;
    float activeTime;
    float inactiveTime;

    IEnumerator activeShieldPowerType;
    
    [SerializeField] GameObject myFace;
    [SerializeField] Sprite[] shieldCreationFacesAnimation;

    public void SetBlinkShieldPower(float startingTime , float activeTime , float inactiveTime)
    {
        this.startingTime = startingTime;
        this.activeTime = activeTime;
        this.inactiveTime = inactiveTime;
        activeShieldPowerType = BlinkShield();
    }

    public void TurnOn()
    {
        shield.SetActive(true);
        if (activeShieldPowerType != null) StartCoroutine(activeShieldPowerType);
    }

    public void TurnOff()
    {
        if (activeShieldPowerType != null) StopCoroutine(activeShieldPowerType);
        shield.SetActive(false);
    }

    private void OnDisable()
    {
        if (activeShieldPowerType != null) StopCoroutine(activeShieldPowerType);
        shield.SetActive(false);
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

    

}
