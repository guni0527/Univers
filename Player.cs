using UnityEngine;
using UnityEngine.SceneManagement;
public class Player : MonoBehaviour
{
    Animator animator = null;
    Rigidbody2D _rigidbody = null;

    public float flapForce = 6f;
    public float forwardSpeed = 3f;
    public bool isDead = false;
    float deathCooldown = 0f;

    bool isGameStarted = false;
    private float originalGravity;
    bool isFlap = false;

    public bool godMode = false;

    GameManager gameManager = null;

    void Start()
    {
        gameManager = GameManager.Instance;

        animator = transform.GetComponentInChildren<Animator>();
        _rigidbody = transform.GetComponent<Rigidbody2D>();

        if (animator == null)
            Debug.LogError("Not Founded Animator");
        if (_rigidbody == null)
            Debug.LogError("Not Founded Rigidbody");

        originalGravity = _rigidbody.gravityScale;
        _rigidbody.gravityScale = 0; // 중력 제거
    }


    void Update()
    {
        if (!isGameStarted)
        {
            if (Input.GetKeyDown(KeyCode.Space))
            {
                isGameStarted = true;
                _rigidbody.gravityScale = originalGravity;
                gameManager.UIManager.startText.gameObject.SetActive(false);
            }

            return;
        }

        if (isDead)
        {
            if (deathCooldown <= 0)
            {
                if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
                {
                    gameManager.RestartGame();
                }

               
                if (Input.GetMouseButtonDown(1)) // 우클릭
                {
                    SceneManager.LoadScene("RPG");
                }
            }
            else
            {
                deathCooldown -= Time.deltaTime;
            }
        }
        else
        {
            if (Input.GetKeyDown(KeyCode.Space) || Input.GetMouseButtonDown(0))
            {
                isFlap = true;
            }
        }
    }





    void FixedUpdate()
    {
        if (isDead || !isGameStarted)
            return;

        Vector3 velocity = _rigidbody.velocity;
        velocity.x = forwardSpeed;

        if (isFlap)
        {
            velocity.y += flapForce;
            isFlap = false;
        }

        _rigidbody.velocity = velocity;

        float angle = Mathf.Clamp((_rigidbody.velocity.y * 10f), -90, 90);
        transform.rotation = Quaternion.Euler(0, 0, angle);
    }




    public void OnCollisionEnter2D(Collision2D collision)
    {
        if (godMode)
            return;

        if (isDead)
            return;

        animator.SetInteger("IsDie", 1);
        isDead = true;
        deathCooldown = 1f;
        gameManager.GameOver();
    }
}