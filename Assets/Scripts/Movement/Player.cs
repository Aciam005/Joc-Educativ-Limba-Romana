using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum KnockBackDirection
{
    up,
    down,
    left,
    right,
}
public class Player : MonoBehaviour
{
    Transform player;
    Rigidbody2D rb;
    public Transform groundCheckPosition;
    UserInput userInput;
    Animator animator;
    Vector3 header;
    HealthMaster healthMaster;

    [Header("Stats")]
    public int health;
    public float speed;
    public float jumpForce;
    public float knockBack;
    [SerializeField] GameObject winScreen;
    [SerializeField] GameObject deathScreen;

    [Space]
    [Header("Debug")]
    [SerializeField] LayerMask IgnoreMe;
    [SerializeField]float xMov;
    [SerializeField] Vector3 rotaion;
    [SerializeField]float yMov;
    [SerializeField] int maxHealth;
    public bool isGrounded;
    public float timeToTakeDamage;
    [SerializeField]float actTimeToTakeDamage;

    [Space]
    [Header("Animations")]
    [SerializeField] bool isRunning;

    [Space]
    [Header("Switches")]
    public bool playerMovementSwitch;
    public bool isPaused;


    void Start()
    {
        player = GetComponent<Transform>();
        userInput = GetComponent<UserInput>();
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        healthMaster = GameObject.FindGameObjectWithTag("Health").GetComponent<HealthMaster>();

        playerMovementSwitch = true;
        isRunning = false;

        xMov = -1;
        actTimeToTakeDamage = 0;
        maxHealth = health;
        healthMaster.SetMaxHealth(maxHealth);
        actTimeToTakeDamage = timeToTakeDamage;
    }

    // Update is called once per frame
    void Update()
    {
        actTimeToTakeDamage -= Time.deltaTime;
        actTimeToTakeDamage = Mathf.Clamp(actTimeToTakeDamage, 0, timeToTakeDamage);
        CheckGround();
        if (playerMovementSwitch)
        {
            HandleMovement();
        }
        handleAnimation();
    }

    private void checkDirection(Vector3 targetPosition)
    {
        header = targetPosition - transform.position;
        float distance = header.magnitude;
        Vector3 direction = header / distance;

        if(direction.x < 0 && isGrounded)
        {
            Debug.Log("LEFT!");
            playerKnockBack(KnockBackDirection.right, knockBack);
            return;
        }
        else {
            if (isGrounded)
            {
                Debug.Log("RIGHT!");
                playerKnockBack(KnockBackDirection.left, knockBack);
                return;
            }
             }
    }

    void HandleMovement()
    {
        rotaion = transform.rotation.eulerAngles;
        if (Input.GetKey(userInput.moveRightKey))
        {
            xMov = 1;

            determineRotation(false);

            isRunning = true;

        }else if (Input.GetKey(userInput.moveLeftKey))
        {
            xMov = 1;

            determineRotation(true);

            isRunning = true;
        }
        else
        {
            xMov = 0;

            isRunning = false;
        }

        if (Input.GetKeyDown(userInput.jumpKey))
        {
            if (CheckGround())
            {
                yMov = 1 * jumpForce;
                rb.AddForce(Vector2.up * jumpForce * Time.fixedDeltaTime,ForceMode2D.Impulse);
            }
            else
            {
                yMov = 0;
            }
        }

        transform.Translate(new Vector3(xMov * speed, 0, 0) * Time.deltaTime);
    }
    bool CheckGround()
    {
        if (Physics2D.Raycast(groundCheckPosition.position, -Vector2.up, 0.01f,~IgnoreMe))
        {
            isGrounded = true;
            return true;
        }
        else
        {
            isGrounded = false;
            return false;
        }
    }

    void determineRotation(bool left)
    {
        if (left)
        {
            transform.eulerAngles = new Vector3(0, -180, 0);
        }else if (!left)
        {
            transform.eulerAngles = new Vector3(0, 0, 0);
        }
    }

    void handleAnimation()
    {
        animator.SetBool("isRunning", isRunning);
    }

    public void playerKnockBack(KnockBackDirection direction,float forceAmount)
    {
        switch (direction) {
            case KnockBackDirection.up:rb.AddForce(Vector2.up * forceAmount * Time.deltaTime, ForceMode2D.Impulse);
                break;

            case KnockBackDirection.down:rb.AddForce(Vector2.down * forceAmount * Time.deltaTime, ForceMode2D.Impulse);
                break;

            case KnockBackDirection.left:rb.AddForce(Vector2.left * forceAmount * Time.deltaTime, ForceMode2D.Impulse);
                break;

            case KnockBackDirection.right:rb.AddForce(Vector2.right * forceAmount * Time.deltaTime, ForceMode2D.Impulse);
                break;
        }
    }

    public void TakeDamage()
    {
        if(actTimeToTakeDamage <= 0)
        {
            health -= 1;
            if (health <= 0)
            {
                die();
            }
            actTimeToTakeDamage = timeToTakeDamage;
            healthMaster.DecreaseHealth();
        }
    }

    public void die()
    {
        playerMovementSwitch = false;
        deathScreen.gameObject.SetActive(true);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            TakeDamage();
            checkDirection(collision.transform.position);
            Debug.Log("YES");
        }if (collision.CompareTag("Win"))
        {
            playerMovementSwitch = false;
            winScreen.gameObject.SetActive(true);
        }
    }
}
