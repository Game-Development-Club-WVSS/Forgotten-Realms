using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Floating : MonoBehaviour
{
    public float upDownSpeed = 2;
    public float amplitude = 0.04f;
    float cnt = 0, origin;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position.y;
    }

    // Update is called once per frame
    void Update()
    {
        cnt += upDownSpeed * Time.deltaTime;
        transform.position = new Vector2(transform.position.x, origin + Mathf.Sin(cnt) * amplitude);
    }
}
