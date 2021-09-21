using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AnimationController
{
    public static IEnumerator FixedCicle(GameObject subject , Sprite[] spriteSheets , float frequency)
    {
        foreach(Sprite spr in spriteSheets)
        {

            yield return new WaitForSeconds(frequency);
            subject.GetComponent<SpriteRenderer>().sprite = spr;
        }
    }

    public static IEnumerator FixedCicle(GameObject subject, Sprite[] spriteSheets, float frequency , float startingTimer)
    {
        yield return new WaitForSeconds(startingTimer);
        foreach (Sprite spr in spriteSheets)
        {

            yield return new WaitForSeconds(frequency);
            subject.GetComponent<SpriteRenderer>().sprite = spr;
        }
    }

    public static IEnumerator LoopingCicle(GameObject subject, Sprite[] spriteSheets, float frequency)
    {
        int counter = 0;
        while (true)
        {
            counter++;
            if (counter == spriteSheets.Length)
            {
                counter = 0;
            }
            subject.GetComponent<SpriteRenderer>().sprite = spriteSheets[counter];
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
            if(counter == spriteSheets.Length)
            {
                counter = 0;
            }
            yield return new WaitForSeconds(frequency);
        }
    }


    public static IEnumerator BlinkAnimation(GameObject subject, Sprite firstSprite, Sprite secondSprite, float duration)
    {
        subject.GetComponent<SpriteRenderer>().sprite = secondSprite;
        yield return new WaitForSeconds(duration);
        subject.GetComponent<SpriteRenderer>().sprite = firstSprite;
    }
    /*
    public static IEnumerator FadeIn(float time, Sprite element)
    {
        float timeCounter = 0;

        while()

    }
    */

    public static IEnumerator GoIn(GameObject subject, float frequency)
    {
        float amount = 0;
        while(amount < 1)
        {
            yield return new WaitForSeconds(frequency);
            subject.transform.localScale = new Vector3(amount, 1, 1);
            amount += frequency*2;
        }
    }


    public static IEnumerator Glow(float frequency, GameObject element , Color start , Color end)
    {
        Color col = start;
        bool down = true;

            while (true)
        {
            yield return new WaitForSeconds(frequency);
            if (col.g >= end.g)
            {
                down = false;
            }
            else if (col.g <= start.g)
            {
                down = true;
            }

            if (down)
            {
                col.g--;
                col.r--;
            }
            else
            {
                col.g++;
                col.r++;
            }
            element.GetComponent<SpriteRenderer>().color = col;
        }

    }


}
