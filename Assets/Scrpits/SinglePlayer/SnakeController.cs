using System.Collections;
using System.Collections.Generic;
using System.Threading;
using System.Timers;
using UnityEngine;

public class SnakeController : MonoBehaviour
{
    private Vector2Int _direction = Vector2Int.right;
    [SerializeField] private int speed, speedMultiplier;

    public GameObject _snakeTailPrefab;
    public int initialSize = 4;
    private float nextUpdate;
    public BoxCollider2D gridArea;
    [SerializeField] private int powerUpDuration = 5;
    private bool hasShield = false;
    public UiController uiController;
    [SerializeField] private List<Transform> _snakeTail;

    void Awake()
    {
        //uiController = GetComponent<UiController>();
    }
    void Start()
    {

        //Time.timeScale = 1;
        ResetState();
    }
    void Update()
    {
        SnakeInput();
        ScreenWrap();
    }
    void FixedUpdate()
    {
        FollowHead();
        SnakeMovement();
    }

    void ScreenWrap()
    {
        Bounds bounds = this.gridArea.bounds;

        if (this.transform.position.x > bounds.max.x)
        {
            transform.position = new Vector2(bounds.min.x, transform.position.y);
        }
        else if (this.transform.position.x < bounds.min.x)
        {
            transform.position = new Vector2(bounds.max.x, transform.position.y);
        }
        else if (this.transform.position.y > bounds.max.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.min.y);
        }
        else if (this.transform.position.y < bounds.min.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.max.y);
        }
    }
    void SnakeInput()
    {
        // Only allow turning up or down while moving in the x-axis
        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                _direction = Vector2Int.up;
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                _direction = Vector2Int.down;
            }
        }
        // Only allow turning left or right while moving in the y-axis
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(KeyCode.RightArrow))
            {
                _direction = Vector2Int.right;
            }
            else if (Input.GetKeyDown(KeyCode.LeftArrow))
            {
                _direction = Vector2Int.left;
            }
        }
    }
    void SnakeMovement()
    {
        // Move the snake in the direction it is facing
        // Round the values to ensure it aligns to the grid
        int moveX = Mathf.RoundToInt(transform.position.x) + _direction.x;
        int moveY = Mathf.RoundToInt(transform.position.y) + _direction.y;
        transform.position = new Vector2(moveX, moveY);

        // Set the next update time based on the speed
        nextUpdate = Time.time + (1f / (speed * speedMultiplier));

    }


    void FollowHead()
    {
        for (int i = _snakeTail.Count - 1; i > 0; i--)
        {
            _snakeTail[i].position = _snakeTail[i - 1].position;
        }
    }

    void ResetState()
    {
        _direction = Vector2Int.right;
        transform.position = Vector3.zero;
        // Start at 1 to skip destroying the head
        for (int i = 1; i < _snakeTail.Count; i++)
        {
            Destroy(_snakeTail[i].gameObject);

        }
        // Clear the list but add back this as the head
        _snakeTail.Clear();
        _snakeTail.Add(this.transform);

        // -1 since the head is already in the list
        for (int i = 0; i < initialSize - 1; i++)
        {
            SnakeGrow();
        }
    }

    void SnakeGrow()
    {

        GameObject snakeTail = Instantiate(_snakeTailPrefab);
        snakeTail.layer = LayerMask.NameToLayer("SnakeTail");
        snakeTail.transform.position = _snakeTail[_snakeTail.Count - 1].position;

        _snakeTail.Add(snakeTail.transform);
    }
    void SnakeShrink()
    {
        if (_snakeTail.Count > 2)
        {
            Destroy(_snakeTail[_snakeTail.Count - 1].gameObject);
            _snakeTail.RemoveAt(_snakeTail.Count - 1);
        }
        else if (_snakeTail.Count <= 2)
        {
            Debug.Log("Snake Died!");
            uiController.OnGameOver();
            Time.timeScale = 0;
        }



    }

    IEnumerator GetShieldPower()
    {
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("SnakeTail"), true);
        yield return new WaitForSeconds(powerUpDuration);
        Debug.Log(hasShield + "getghield");
        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("SnakeTail"), false);

    }
    IEnumerator SpeedBoost()
    {
        Time.fixedDeltaTime = 0.12f;
        yield return new WaitForSeconds(powerUpDuration);
        Time.fixedDeltaTime = 0.2f;

    }
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("GreenApple"))
        {
            SnakeGrow();
            uiController.UpdateScore();
            //score++ 

        }
        if (other.CompareTag("RedApple"))
        {
            SnakeShrink();
        }
        if (other.CompareTag("SpeedUp"))
        {
            //speed up
            StartCoroutine(SpeedBoost());

        }
        if (other.CompareTag("ScoreBoost"))
        {
            uiController.isScoreBoost = true;
            uiController.UpdateScore();

        }

        if (other.CompareTag("SnakeTail"))
        {
            //game Over 

            Debug.Log(hasShield);
            uiController.OnGameOver();
            Time.timeScale = 0;

        }
        if (other.CompareTag("Shield"))
        {
            hasShield = true;
            Debug.Log(hasShield + "trigger");
            StartCoroutine(GetShieldPower());

            // Shield Code
        }
    }

}// class ends
