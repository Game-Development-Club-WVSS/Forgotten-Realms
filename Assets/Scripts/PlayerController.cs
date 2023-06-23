using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    public float speed = 1.0f;
    public float jumpForce = 2.5f;
    public float doubleJumpForce = 1.5f;
    public bool isGrounded = false;
    public GameObject bullet;
    public float shootOffsetY = 0;
    public float crouchShootOffsetY = 0;
    public Image healthBar;
    public Sprite[] healthBarSprites;
    public string  initialScene = "Level 1";
    public string winScene = "Win";
    public string[] sceneName;
    public Vector2[] originPosition;
    public int keyHave;
    HealthSystem healthSystem;
    Animator animator;
    Dictionary<string, Vector2> origin;
    bool jump = false;
    bool shooting = false;
    bool doubleJump = false;

    // Start is called before the first frame update
    void Start()
    {
        healthSystem = this.GetComponent<HealthSystem>();
        animator = this.GetComponent<Animator>();

        SceneManager.LoadScene(initialScene);

        origin = new Dictionary<string, Vector2>();
        for (int i = 0; i < Mathf.Min(sceneName.Length, originPosition.Length); i++)
        {
            origin[sceneName[i]] = originPosition[i];
        }

        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    // Update is called once per frame
    void Update()
    {
        // ------------------------------------------------------------------------------ Action System ------------------------------------------------------------------------------
        if(isGrounded && Input.GetKeyDown(KeyCode.Space))
        {
            animator.SetTrigger("shoot");
            shooting = true;
        }

        bool run = false;
        Vector2 velocityX = Vector2.zero;
        if ((Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.A)) && !shooting)
        {
            velocityX += Vector2.left * speed * Time.deltaTime;
            transform.localScale = new Vector3(-Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            run = true;
        }
        if ((Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.D)) && !shooting)
        {
            velocityX += Vector2.right * speed * Time.deltaTime;
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            run = true;
        }

        if ((Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.W)) && (isGrounded || !doubleJump))
        {
            jump = true;
            if (!isGrounded) doubleJump = true;
            else doubleJump = false;
        }

        if (jump && !shooting)
        {
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if(isGrounded) GetComponent<Rigidbody2D>().AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
            else GetComponent<Rigidbody2D>().AddForce(Vector2.up * doubleJumpForce, ForceMode2D.Impulse);
            animator.SetBool("jump", true);
            isGrounded = false;
            run = false;
            jump = false;
        }

        animator.SetBool("run", run);

        if ((Input.GetKey(KeyCode.DownArrow) || Input.GetKey(KeyCode.S)) && isGrounded && !run)
        {
            animator.SetBool("crouch", true);
        }
        else
        {
            animator.SetBool("crouch", false);
        }

        if (!isGrounded && GetComponent<Rigidbody2D>().velocity.y < 0)
        {
            animator.SetBool("fall", true);
        }
        else
        {
            animator.SetBool("fall", false);
        }

        if (run)
        {
            transform.Translate(velocityX);
        }

        // ------------------------------------------------------------------------------ Health System ------------------------------------------------------------------------------
        if (transform.position.y < -5)
        {
            if (origin.ContainsKey(SceneManager.GetActiveScene().name))
                transform.position = new Vector3(origin[SceneManager.GetActiveScene().name].x, origin[SceneManager.GetActiveScene().name].y, transform.position.z);
            else
                transform.position = new Vector3(0, 0, transform.position.z);
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
            GetComponent<Rigidbody2D>().velocity = Vector2.zero;
            if (SceneManager.GetActiveScene().name != winScene)
            {
                this.GetComponent<HealthSystem>().HealthChange(-1);
            }
        }

        if (SceneManager.GetActiveScene().name == winScene)
        {
            return;
        }

        float healthPercent = (float)healthSystem.Health / (float)healthSystem.MaxHealth;
        if (healthPercent > 0.8f)
        {
            healthBar.sprite = healthBarSprites[0];
        }
        else if (healthPercent > 0.6f)
        {
            healthBar.sprite = healthBarSprites[1];
        }
        else if (healthPercent > 0.4f)
        {
            healthBar.sprite = healthBarSprites[2];
        }
        else if (healthPercent > 0.2f)
        {
            healthBar.sprite = healthBarSprites[3];
        }
        else if (healthPercent > 0)
        {
            healthBar.sprite = healthBarSprites[4];
        }
        else
        {
            healthBar.sprite = healthBarSprites[5];
        }

        if (healthSystem.Health <= 0)
        {
            healthSystem.Health = healthSystem.MaxHealth;
            SceneManager.LoadScene(initialScene);
        }
    }

    public void StopShooting()
    {
        shooting = false;
    }

    public void CrouchShoot()
    {
        Vector2 position = transform.position;
        position.y += crouchShootOffsetY;
        Instantiate(bullet, position, Quaternion.identity).transform.localScale = transform.localScale;
    }

    public void Shoot()
    {
        Vector2 position = transform.position;
        position.y += shootOffsetY;
        Instantiate(bullet, position, Quaternion.identity).transform.localScale = transform.localScale;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Coin"))
        {
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Key"))
        {
            keyHave++;
            Destroy(other.gameObject);
        }
        if (other.gameObject.CompareTag("Prize"))
        {
            Destroy(other.gameObject);
        }
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        keyHave = 0;
        if(origin.ContainsKey(scene.name)) transform.position = new Vector3(origin[scene.name].x, origin[scene.name].y, transform.position.z);
        else transform.position = new Vector3(0, 0, transform.position.z);
        transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);
        GetComponent<Rigidbody2D>().velocity = Vector2.zero;
    }

    void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }
}
