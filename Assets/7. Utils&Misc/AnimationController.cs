﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationController
{
    public static IEnumerator FixedCicle(GameObject subject , Sprite[] spriteSheets , float frequency)
    {
        foreach(Sprite spr in spriteSheets)
        {
            subject.GetComponent<SpriteRenderer>().sprite = spr;
            yield return new WaitForSeconds(frequency);
        }
    }

    public static IEnumerator FixedCicle(GameObject subject, Sprite[] spriteSheets, float frequency , float startingTimer)
    {
        yield return new WaitForSeconds(startingTimer);
        foreach (Sprite spr in spriteSheets)
        {
            subject.GetComponent<SpriteRenderer>().sprite = spr;
            yield return new WaitForSeconds(frequency);
        }
    }

    public static IEnumerator LoopingCicle(GameObject subject, Sprite[] spriteSheets, float frequency)
    {
        int counter = 0;
        while (true)
        {
            subject.GetComponent<SpriteRenderer>().sprite = spriteSheets[counter];
            counter++;
            yield return new WaitForSeconds(frequency);
        }
    }

    public static IEnumerator LoopingCicle(GameObject subject, Sprite[] spriteSheets, float frequency, float startingTimer)
    {
        int counter = 0;
        yield return new WaitForSeconds(startingTimer);
        while (true)
        {
            subject.GetComponent<SpriteRenderer>().sprite = spriteSheets[counter];
            counter++;
            yield return new WaitForSeconds(frequency);
        }
    }
    
    public static IEnumerator BlinkAnimation(GameObject subject, Sprite first, Sprite second, float frequency)
    {
        subject.GetComponent<SpriteRenderer>().sprite = second;
        yield return new WaitForSeconds(frequency);
        subject.GetComponent<SpriteRenderer>().sprite = first;
    }


}
