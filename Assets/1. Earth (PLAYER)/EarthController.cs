using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 startingPosition;
    public bool isPlaying;

    private void Start()
    {
        startingPosition = gameObject.transform.position;
        isPlaying = false;
    }

    public void ResetEarthPosition()
    {
        gameObject.transform.SetPositionAndRotation(startingPosition, Quaternion.identity);
    }

    public void GoPlay(bool go)
    {
        isPlaying = go;
    }
    private void Update()
    {
        if (Input.touches.Length > 0 && isPlaying)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.touches[i];
                if(touch.phase  != TouchPhase.Began)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    if(worldPosition.x> -2.4 && worldPosition.x < 2.4)
                    this.transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
                }
            }

        }

        if (Input.GetMouseButton(0) && isPlaying)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (worldPosition.x > -2.4 && worldPosition.x < 2.4)
                this.transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
        }
    }

}
