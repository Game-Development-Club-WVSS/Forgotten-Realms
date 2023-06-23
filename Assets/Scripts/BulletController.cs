using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed = 5;
    Camera mainCamera;
    float origin;

    // Start is called before the first frame update
    void Start()
    {
        mainCamera = Camera.main;
        origin = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(Vector2.right * transform.localScale * speed * Time.deltaTime);

        Vector3 viewportPos = mainCamera.WorldToViewportPoint(gameObject.transform.position);
        if (viewportPos.x < 0 || viewportPos.x > 1 || viewportPos.y < 0 || viewportPos.y > 1)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Enemy"))
        {
            other.gameObject.GetComponent<HealthSystem>().HealthChange(-1);
            if (other.gameObject.GetComponent<HealthSystem>().Health <= 0)
            {
                other.gameObject.GetComponent<EnemyController>().Die();
            }
            Destroy(gameObject);
        }
    }
}
