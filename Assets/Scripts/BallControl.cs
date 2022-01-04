using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class BallControl : MonoBehaviour
{
    private Rigidbody2D rigidBody2D;
    private new SpriteRenderer renderer;
    private BallSoundController sound;
    private Vector2 direction;
    private bool clickable;

    public ScoreController score;
    public TextMesh textMesh;
    public Canvas canvas;
    public Slider slider;

    public float speed = 5;
    public float clickCD = 5;
    private float _clickCD;

    // Start is called before the first frame update
    void Start()
    {
        Physics2D.IgnoreLayerCollision(0, 5);
        rigidBody2D = GetComponent<Rigidbody2D>();
        renderer = GetComponent<SpriteRenderer>();
        sound = GetComponent<BallSoundController>();
        clickable = true;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBall();

        //ArrowMovement();

        ClickMovement();

        if (clickable)
        {
            renderer.color = Color.black;
            textMesh.text = "";
        }
        else
        {
            renderer.color = Color.red;
            textMesh.text = ((int)_clickCD + 1).ToString();
        }
    }

    void MoveBall()
    {
        rigidBody2D.velocity = direction * speed;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        string tag = collision.gameObject.tag;
        if (tag == "Wall" || tag == "Square")
        {
            direction = Vector2.Reflect(direction, collision.contacts[0].normal);

            if (tag == "Wall")
                sound.PlayWall();
        }

        if (tag == "Square")
        {
            score.IncreaseCurrentScore(score.squareScore);
            collision.gameObject.GetComponent<Square>().Destroy();
            slider.value++;

            sound.PlaySquare();

            GameManager.Instance.CollectByTap(tag, collision.gameObject.transform.position, canvas.transform);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Coin")
        {
            score.IncreaseCurrentScore(score.coinScore);
            GameManager.Instance.CollectByTap(collision.gameObject.tag, collision.gameObject.transform.position, canvas.transform);
            CoinSpawner.Instance.DestroyCoin(collision.gameObject);
            slider.value++;

            sound.PlayCoin();
        }
    }

    private void ArrowMovement()
    {
        if (Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            direction += Vector2.left;

            if (direction == Vector2.zero || direction.x < Vector2.left.x)
                direction = Vector2.left;
        }

        if (Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.DownArrow))
        {
            direction += Vector2.down;

            if (direction == Vector2.zero || direction.y < Vector2.down.y)
                direction = Vector2.down;
        }

        if (Input.GetKeyDown(KeyCode.D) || Input.GetKeyDown(KeyCode.RightArrow))
        {
            direction += Vector2.right;

            if (direction == Vector2.zero || direction.x > Vector2.right.x)
                direction = Vector2.right;
        }

        if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.UpArrow))
        {
            direction += Vector2.up;

            if (direction == Vector2.zero || direction.y > Vector2.up.y)
                direction = Vector2.up;
        }
    }

    private void ClickMovement()
    {
        _clickCD -= Time.unscaledDeltaTime;
        if (_clickCD <= 0f)
        {
            clickable = true;
            _clickCD = 0f;
        }

        Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 currentPos = transform.position;

        if (clickable && Input.GetMouseButtonDown(0))
        {
            if(!EventSystem.current.IsPointerOverGameObject())
            {
                sound.PlayShoot();

                if (Vector2.Distance(mousePos, currentPos) > 0.1f)
                {
                    direction = (mousePos - currentPos).normalized;
                    clickable = false;
                    _clickCD = clickCD;
                }
            }
        }
    }

    public void FreezeBall()
    {
        if (slider.value == 10)
        {
            direction = Vector2.zero;
            slider.value = 0;
            sound.PlayFreeze();
        }
    }
}
