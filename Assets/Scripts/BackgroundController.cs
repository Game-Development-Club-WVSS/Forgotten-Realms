using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundController : MonoBehaviour
{
    public Transform Camera;
    public float relevantSpeed = 0.5f;
    float offsetX, offsetY;

    // Start is called before the first frame update
    void Start()
    {
        offsetX = transform.position.x;
        offsetY = transform.position.y;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        float dx = Camera.position.x * relevantSpeed;
        float z = transform.parent.localPosition.z;
        transform.position = new Vector3(Camera.position.x + offsetX + dx, Camera.position.y * 0.8f / (4 - z) + offsetY, transform.position.z);

        if(transform.position.x - Camera.position.x > Screen.width/100f)
        {
            offsetX -= Screen.width / 50f;
        }

        if (transform.position.x - Camera.position.x < Screen.width / -100f)
        {
            offsetX += Screen.width / 50f;
        }
    }
}
