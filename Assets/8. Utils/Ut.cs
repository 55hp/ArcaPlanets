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


}
