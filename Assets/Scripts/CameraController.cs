using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CameraController : MonoBehaviour
{
    public Transform player;
    public float followingSpeedX = 0.2f;
    public float followingSpeedY = 0.2f;
    public float offsetY = -0.5f;

    // Start is called before the first frame update
    void Start()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        float dx = (player.position.x - transform.position.x) * followingSpeedX;
        float dy = (player.position.y - offsetY - transform.position.y) * followingSpeedY;
        transform.Translate(new Vector2(dx, dy) * Time.deltaTime);
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        transform.position = new Vector3(player.position.x, player.position.y - offsetY, transform.position.z);
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
