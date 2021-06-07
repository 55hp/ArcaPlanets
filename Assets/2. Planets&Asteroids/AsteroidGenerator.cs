using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidGenerator : MonoBehaviour
{

    [SerializeField] GameObject asteroidPrefab;
    // Start is called before the first frame update
    void Start()
    {
        InvokeRepeating("GenAst",1f,4f);
    }


    public void GenAst()
    {
        Instantiate(asteroidPrefab, new Vector3(-5,Random.Range(-1,2),0) , Quaternion.identity);
    }

}
