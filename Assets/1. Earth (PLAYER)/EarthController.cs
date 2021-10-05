using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EarthController : MonoBehaviour
{
    [SerializeField] float speed;
    Vector3 startingPosition;
    public bool isPlaying;

    float min;
    float max;

    private void Start()
    {
        min = -((Camera.main.orthographicSize * Camera.main.aspect));
        max = (Camera.main.orthographicSize * Camera.main.aspect);

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
        lastPosition = this.transform.position;

        if (Input.touches.Length > 0 && isPlaying)
        {
            for(int i = 0; i < Input.touchCount; i++)
            {
                var touch = Input.touches[i];
                if(touch.phase  != TouchPhase.Began)
                {
                    Vector3 worldPosition = Camera.main.ScreenToWorldPoint(touch.position);
                    if(worldPosition.x> min && worldPosition.x < max)
                        this.transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
                }
            }
        }

        if (Input.GetMouseButton(0) && isPlaying)
        {
            Vector3 worldPosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            if (worldPosition.x > min && worldPosition.x < max)
                this.transform.position = new Vector3(worldPosition.x, transform.position.y, transform.position.z);
        }

        
        // Aggiorna la rotazione
        rotation = Mathf.MoveTowardsAngle(rotation, lastPosition.x - this.transform.position.x, 0.1f * Time.deltaTime);
        Rotate20(rotation); 
    }
    private Vector3 lastPosition;

    float rotation = 0;

    Quaternion startingRotation = Quaternion.Euler(0, 0, 0);
    Quaternion rightRotation = Quaternion.Euler(0, 0, 20);
    Quaternion leftRotation = Quaternion.Euler(0, 0, -20);

    //Rotate the Earthship by 20 or -20 degrees on the Z axis when moving to the left or to the right
    public void Rotate20(float dir)
    { 
        if(dir > 0) //Moving to the right
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, rightRotation, 0.1f);
        else if (dir < 0 ) //Moving to the left
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, leftRotation, 0.1f);
        else if(dir == 0)
            this.transform.rotation = Quaternion.Slerp(this.transform.rotation, startingRotation, 0.1f);
    }


}
