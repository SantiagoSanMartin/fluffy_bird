using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerControl : MonoSingleton<PlayerControl>
{
    [SerializeField] private float force;

    private bool hasControl = true;
    private bool isClicking;
    private Rigidbody2D rg;
    private Animator birdAnimator;
    private float maxSpeed;
    private Coroutine starCoroutine;

    private const string GROUND_TAG = "Ground";
    private const string OBSTACLE_TAG = "Obstacle";
    private const string STAR_TAG = "Star";
    private const string POINT_TAG = "Point";
    private const float MAX_HEIGHT = 5f;


    protected override void Awake()
    {
        base.Awake();
        rg = GetComponent<Rigidbody2D>();
        birdAnimator = GetComponent<Animator>();
        starCoroutine = StartCoroutine(Star());
        StopAllCoroutines();
    }

    private void OnEnable()
    {
        maxSpeed = 8f;
        transform.position = Vector3.zero;
        rg.bodyType = RigidbodyType2D.Dynamic;
        hasControl = true;
    }

    private void OnDisable()
    {
        birdAnimator.SetBool("IsDead", false);
        birdAnimator.SetBool("IsStar", false);
        StopAllCoroutines();
    }

    void Update()
    {
        if (hasControl && Input.GetMouseButtonDown(0) && !isClicking)
        { 
            isClicking = true;
        }

        if(transform.position.y > MAX_HEIGHT)
        {
            StartCoroutine(Collision());
        }
    }

    private void FixedUpdate()
    {
        if (isClicking && hasControl)
        {
            isClicking = false;
            rg.AddForce(Vector3.up * force);

            if (rg.velocity.x < maxSpeed)
            {
                rg.AddForce(Vector3.right * force * 0.15f);
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.collider.CompareTag(GROUND_TAG))
        {
            StartCoroutine(Collision());
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag(OBSTACLE_TAG))
        {
            if (!birdAnimator.GetBool("IsStar"))
            {
                StartCoroutine(Collision());
            }
        }
        else if (collision.CompareTag(STAR_TAG))
        {
            StopCoroutine(starCoroutine);
            starCoroutine = StartCoroutine(Star());
            collision.gameObject.SetActive(false);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.CompareTag(POINT_TAG))
        {
            GameManager.Instance.AddPoint();
            maxSpeed += 0.05f;
        }
    }

    private IEnumerator Star()
    {
        yield return null;

        birdAnimator.SetBool("IsStar", true);

        yield return new WaitForSeconds(10f);

        birdAnimator.SetBool("IsStar", false);
    }

    private IEnumerator Collision()
    {
        rg.velocity = Vector3.zero;
        rg.bodyType = RigidbodyType2D.Static;
        hasControl = false;
        birdAnimator.SetBool("IsDead", true);

        yield return new WaitForSeconds(3f);

        GameManager.Instance.GameOver();
    }
}
