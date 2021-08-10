using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class Ut 
{
    //This method Randomly returns an Object of type <T> from an Array of the same type.
    public static T ROA<T>(T[] array)
    {
        return array[Random.Range(0, array.Length)];
    }
    


    public static bool TossCoin()
    {
        bool coin;
        int x = Random.Range(0, 2);
        if(x == 0)
        {
            coin = true;
        }
        else
        {
            coin = false;
        }

        return coin;
    }
}
