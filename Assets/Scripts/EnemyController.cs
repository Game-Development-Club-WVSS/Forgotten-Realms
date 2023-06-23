using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public float speed = 0.3f;
    public float maxLeft=0.3f;
    public float maxRight= 0.3f;
    public GameObject deathAnimation;
    public float deathAnimationOffsetY = 0;
    float origin;
    Vector2 direction = Vector2.left;

    // Start is called before the first frame update
    void Start()
    {
        origin = transform.position.x;
    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(direction * speed * Time.deltaTime);

        if(transform.position.x < origin - maxLeft && direction == Vector2.left)
        {
            direction = Vector2.right;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }

        if (transform.position.x > origin + maxRight && direction == Vector2.right)
        {
            direction = Vector2.left;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        }
    }

    public void Die()
    {
        Vector2 position = new Vector2(transform.position.x, transform.position.y + deathAnimationOffsetY);
        Instantiate(deathAnimation, position, Quaternion.identity);
        Destroy(gameObject);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            other.gameObject.GetComponent<HealthSystem>().HealthChange(-1);
            other.gameObject.GetComponent<Animator>().SetTrigger("hurt");
        }
    }
}
