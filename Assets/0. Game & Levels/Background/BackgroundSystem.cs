using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundSystem : MonoBehaviour
{


    [SerializeField] Color[] skyColors;

    [SerializeField] GameObject clusterPref;
    [SerializeField] Sprite[] clusters;

    [SerializeField] GameObject nebulaPref;
    [SerializeField] Sprite[] nebulae;
    [SerializeField] Color[] nebulaeColors;


    public void InitSky()
    {
        gameObject.GetComponent<SpriteRenderer>().color = Ut.ROA(skyColors);

        StartCoroutine(NebulaGen(5));

    }

    IEnumerator NebulaGen(float time)
    {
        while (true)
        {
            yield return new WaitForSeconds(time);
            var newNebula = Instantiate(nebulaPref, gameObject.transform.position, Quaternion.identity);
            SetColorAndType(newNebula);
        }
    }


    private void Update()
    {
        
    }

    public void SetColorAndType(GameObject nebula)
    {
        nebula.GetComponent<SpriteRenderer>().sprite = Ut.ROA(nebulae);
        nebula.GetComponent<SpriteRenderer>().color = Ut.ROA(nebulaeColors);
    }


}
