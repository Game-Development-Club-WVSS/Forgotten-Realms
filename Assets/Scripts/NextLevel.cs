using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextLevel : MonoBehaviour
{
    public string nextLevel;
    public int keyNeeded = 0;
    public float MaxDistanceToPlayer = 0.05f;
    public Sprite doorClosed;
    public Sprite doorOpened;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player") )
        {
            PlayerController playerController = other.GetComponent<PlayerController>();
            if (playerController != null && playerController.keyHave >= keyNeeded)
            {
                GetComponent<SpriteRenderer>().sprite = doorOpened;
            }
        }
    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Player"))
        {
            if (GetComponent<SpriteRenderer>().sprite == doorOpened)
            {
                if (Mathf.Abs(other.transform.position.x - transform.position.x) < MaxDistanceToPlayer)
                {
                    SceneManager.LoadScene(nextLevel);
                }
            }
        }
    }
}
