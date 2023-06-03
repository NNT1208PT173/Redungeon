using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : Singleton<PlayerMoveController>
{
    [SerializeField] private float _moveDuration = 0.07f;

    public ContactFilter2D castFilter;

    Rigidbody2D rb;
    CapsuleCollider2D capsuleCol;

    RaycastHit2D[] wallHits = new RaycastHit2D[5];

    private bool _isMoving = false;
    public bool IsMoving
    {
        get
        {
            return _isMoving;
        }

        private set
        {
            _isMoving = value;
        }
    }

    private Vector3 _direction;
    public Vector3 Direction
    {
        get
        {
            return _direction;
        }

        private set
        {
            _direction = value;
        }
    }

    private Vector3 _position;
    public Vector3 Position
    {
        get
        {
            return _position;
        }

        private set
        {
            _position = value;

        }
    }

    private bool _isAlive = true;
    public bool IsAlive
    {
        get
        {
            return _isAlive;
        }
        private set
        {
            _isAlive = value;
            if (value == false)
            {
                InputEvents.exitEvent.Invoke();
            }
        }
    }

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        capsuleCol = GetComponent<CapsuleCollider2D>();
    }


    private void Start()
    {
        InputEvents.moveRightEvent.AddListener(OnMoveRightEvent);
        InputEvents.moveLeftEvent.AddListener(OnMoveLeftEvent);
        InputEvents.moveForwardEvent.AddListener(OnMoveForwardEvent);
        InputEvents.moveBackwardEvent.AddListener(OnMoveBackwardEvent);
    }

    private void Update()
    {

    }

    private void OnDestroy()
    {
        InputEvents.moveRightEvent.RemoveListener(OnMoveRightEvent);
        InputEvents.moveLeftEvent.RemoveListener(OnMoveLeftEvent);
        InputEvents.moveForwardEvent.RemoveListener(OnMoveForwardEvent);
        InputEvents.moveBackwardEvent.RemoveListener(OnMoveBackwardEvent);
    }

    private void OnMoveRightEvent()
    {
        if (!IsMoving && CanMoveInDirection(Vector2.right))
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.right));
        }
    }

    private void OnMoveLeftEvent()
    {
        if (!IsMoving && CanMoveInDirection(Vector2.left))
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.left));
        }
    }

    private void OnMoveForwardEvent()
    {
        if (!IsMoving && CanMoveInDirection(Vector2.up))
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.up));
        }
    }

    private void OnMoveBackwardEvent()
    {
        if (!IsMoving && CanMoveInDirection(Vector2.down))
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.down));
        }
    }

    private IEnumerator Move(Vector3 startPosition, Vector3 targetPosition)
    {
        Direction = targetPosition - startPosition;

        Vector3 roundedPosition = new Vector3(
            Mathf.Round(transform.position.x * 2f) / 2f,
            Mathf.Round(transform.position.y * 2f) / 2f,
            Mathf.Round(transform.position.z * 2f) / 2f
        );
        Position = roundedPosition;
        IsMoving = true;
        float duration = _moveDuration;
        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime / _moveDuration);
            elapsedTime += Time.deltaTime;
            yield return null;
        }
        roundedPosition = new Vector3(
            Mathf.Round(transform.position.x * 2f) / 2f,
            Mathf.Round(transform.position.y * 2f) / 2f,
            Mathf.Round(transform.position.z * 2f) / 2f
        );
        transform.position = roundedPosition;
        IsMoving = false;
    }

    private bool CanMoveInDirection(Vector2 direction)
    {
        bool canMove = capsuleCol.Cast(direction, castFilter, wallHits, 0.2f) > 0;
        return !canMove;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "DeathZone")
        {
            IsAlive = false;
        }
    }
}
