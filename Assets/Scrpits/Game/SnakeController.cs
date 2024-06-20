using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SnakeController : MonoBehaviour
{

    public enum PlayerType { GreenSnake, YellowSnake }
    [SerializeField] private PlayerType playerType;
    private Vector2Int _direction = Vector2Int.right;
    [SerializeField] private int speed;
    [SerializeField] private float speedMultiplier = 1f;
    [SerializeField] private GameObject snakeTailPrefab;
    public int initialSize = 4;
    public BoxCollider2D gridArea;
    [SerializeField] private int powerUpDuration = 5;
    private bool hasShield = false;
    public UiController uiController;
    [SerializeField] private List<Transform> snakeTail;

    private float nextUpdate;

    void Start()
    {
        ResetState();
    }

    void Update()
    {
        HandleInput();
        ScreenWrap();
    }

    void FixedUpdate()
    {
        MoveSnake();
       
    }

    void HandleInput()
    {
        KeyCode upKey, downKey, leftKey, rightKey;

        switch (playerType)
        {
            case PlayerType.GreenSnake:
                upKey = KeyCode.UpArrow;
                downKey = KeyCode.DownArrow;
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                break;
            case PlayerType.YellowSnake:
                upKey = KeyCode.W;
                downKey = KeyCode.S;
                leftKey = KeyCode.A;
                rightKey = KeyCode.D;
                break;
            default:
                upKey = KeyCode.UpArrow;
                downKey = KeyCode.DownArrow;
                leftKey = KeyCode.LeftArrow;
                rightKey = KeyCode.RightArrow;
                break;
        }

        if (_direction.x != 0f)
        {
            if (Input.GetKeyDown(upKey))
            {
                _direction = Vector2Int.up;
                
            }
            else if (Input.GetKeyDown(downKey))
            {
                _direction = Vector2Int.down;
               
            }
        }
        else if (_direction.y != 0f)
        {
            if (Input.GetKeyDown(rightKey))
            {
                _direction = Vector2Int.right;
               
            }
            else if (Input.GetKeyDown(leftKey))
            {
                _direction = Vector2Int.left;
                
            }
        }
    }

    void ScreenWrap()
    {
        Bounds bounds = gridArea.bounds;

        if (transform.position.x > bounds.max.x)
        {
            transform.position = new Vector2(bounds.min.x, transform.position.y);
        }
        else if (transform.position.x < bounds.min.x)
        {
            transform.position = new Vector2(bounds.max.x, transform.position.y);
        }
        else if (transform.position.y > bounds.max.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.min.y);
        }
        else if (transform.position.y < bounds.min.y)
        {
            transform.position = new Vector2(transform.position.x, bounds.max.y);
        }
    }

    void ResetState()
    {
        switch (playerType)
        {
            case PlayerType.GreenSnake:
                _direction = Vector2Int.right;
                transform.position = new Vector3(1, 1, 0);
                break;
            case PlayerType.YellowSnake:
                _direction = Vector2Int.left;
                transform.position = new Vector3(-1, -1, 0);
                break;

        }

        for (int i = 1; i < snakeTail.Count; i++)
        {
            Destroy(snakeTail[i].gameObject);
        }

        snakeTail.Clear();
        snakeTail.Add(transform);

        for (int i = 0; i < initialSize - 1; i++)
        {
            GrowSnake();
        }
    }

    void MoveSnake()
    {
        FollowHead();

        int moveX = Mathf.RoundToInt(transform.position.x) + _direction.x;
        int moveY = Mathf.RoundToInt(transform.position.y) + _direction.y;
        transform.position = new Vector2(moveX, moveY);

        nextUpdate = Time.time + (1f / speed);
    }

    void FollowHead()
    {
        for (int i = snakeTail.Count - 1; i > 0; i--)
        {
            snakeTail[i].position = snakeTail[i - 1].position;
        }
    }

    void GrowSnake()
    {
        GameObject newTail = Instantiate(snakeTailPrefab);
        newTail.layer = LayerMask.NameToLayer("SnakeTail");
        newTail.transform.position = snakeTail[snakeTail.Count - 1].position;
        snakeTail.Add(newTail.transform);


    }

    void ShrinkSnake()
    {
        if (snakeTail.Count > 2)
        {
            Destroy(snakeTail[snakeTail.Count - 1].gameObject);
            snakeTail.RemoveAt(snakeTail.Count - 1);
        }
        else if (snakeTail.Count <= 2)
        {
            Debug.Log("Snake Died!");
            uiController.OnGameOver();
            Time.timeScale = 0;
        }
    }
    IEnumerator GetShieldPower()
    {
        GameManager.Instance.PlaySfx(Sounds.POWER_PICKUP);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("SnakeTail"), true);
        yield return new WaitForSeconds(powerUpDuration);

        Physics2D.IgnoreLayerCollision(LayerMask.NameToLayer("Default"), LayerMask.NameToLayer("SnakeTail"), false);
        hasShield =false;

    }
    IEnumerator SpeedBoost()
    {
        GameManager.Instance.PlaySfx(Sounds.POWER_PICKUP);
        speedMultiplier *= 2;
        yield return new WaitForSeconds(powerUpDuration);
        speedMultiplier /= 2;
    }
    void Die()
    {
        if (hasShield == false)
        {   
            Debug.Log("Snake Died!");
            
            uiController.OnGameOver();
            Time.timeScale = 0;
        }
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        Food food = other.GetComponent<Food>();
        if (food != null)
        {
            switch (food.foodType)
            {
                case Food.FoodType.GreenApple:
                    if (playerType == PlayerType.GreenSnake)
                    {
                        GrowSnake();
                        GameManager.Instance.PlaySfx(Sounds.FOOD_PICKUP);
                        uiController.UpdateGreenSnakeScore();

                    }
                    else if (playerType == PlayerType.YellowSnake)
                    {
                        GameManager.Instance.PlaySfx(Sounds.Gethit);
                        ShrinkSnake();
                        
                    }
                    break;
                case Food.FoodType.RedApple:
                    if (playerType == PlayerType.YellowSnake)
                    {
                        GrowSnake();
                        GameManager.Instance.PlaySfx(Sounds.FOOD_PICKUP);
                        uiController.UpdateYellowSnakeScore();

                    }
                    else if (playerType == PlayerType.GreenSnake)
                    {
                        GameManager.Instance.PlaySfx(Sounds.Gethit);
                        ShrinkSnake();
                    }
                    break;
                case Food.FoodType.ScoreBoost:
                    if (playerType == PlayerType.YellowSnake)
                    {
                        uiController.isScoreBoost = true;
                        uiController.UpdateYellowSnakeScore();


                    }
                    else if (playerType == PlayerType.GreenSnake)
                    {
                        uiController.isScoreBoost = true;
                        uiController.UpdateGreenSnakeScore();
                    }

                    break;
                case Food.FoodType.SpeedUp:
                    StartCoroutine(SpeedBoost());
                    break;

                case Food.FoodType.Shield:
                    hasShield = true;
                    Debug.Log(hasShield + "active trigger");
                    StartCoroutine(GetShieldPower());
                    break;
            }
            Destroy(other.gameObject);
        }
        else if (other.CompareTag("SnakeTail"))
        {
            Die();
        }
        else if (other.CompareTag("Player"))
        {
            if (other.GetComponent<SnakeController>().playerType != playerType)
            {
                {
                    other.GetComponent<SnakeController>().uiController.OnGameOver();
                    Destroy(other.gameObject);
                }
            }
        }
    }
}//class

