﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShieldModule : MonoBehaviour
{
    [SerializeField] Sprite[] shieldSprites;

    [SerializeField] GameObject shield;
    float startingTime;
    float timeRate;
    IEnumerator myPower;

    public enum ShieldType
    {
        BLINK,
        ROTATE90
    }

    public void SetShieldPower(ShieldType type , float startingTime , float fireRate)
    {
        this.startingTime = startingTime;
        this.timeRate = fireRate;

        if(type == ShieldType.BLINK)
        {
            shield.GetComponent<SpriteRenderer>().sprite = shieldSprites[0];
            myPower = BlinkShield();
        }else if(type == ShieldType.ROTATE90)
        {
            shield.GetComponent<SpriteRenderer>().sprite = shieldSprites[1];
            myPower = Rotate90();
        }
    }

    public void TurnOn()
    {
        shield.SetActive(true);
        if (myPower != null) StartCoroutine(myPower);
    }

    public void TurnOff()
    {
        if (myPower != null) StopCoroutine(myPower);
        shield.SetActive(false);
    }

    private void OnDisable()
    {
        if (myPower != null) StopCoroutine(myPower);
        shield.SetActive(false);
    }

    IEnumerator Rotate90()
    {
        yield return new WaitForSeconds(startingTime);
        shield.SetActive(false);
        while (true)
        {
            yield return new WaitForSeconds(timeRate);
            ShieldRotation();
        }
    }


    IEnumerator BlinkShield()
    {
        shield.SetActive(false);
        yield return new WaitForSeconds(startingTime);
        while (true)
        {
            yield return new WaitForSeconds(timeRate);
            ShieldActivationSwitch();
        }
    }

    private void ShieldRotation()
    {
        shield.transform.Rotate(0, 90, 0);
    }


    private void ShieldActivationSwitch()
    {
        if (shield.activeSelf)
        {
            shield.GetComponent<SpriteRenderer>().sprite = null;
            shield.SetActive(false);
        }
        else
        {
            shield.SetActive(true);
            StartCoroutine(AnimationController.FixedCicle(shield, shieldSprites, 0.1f));
        }
    }



}