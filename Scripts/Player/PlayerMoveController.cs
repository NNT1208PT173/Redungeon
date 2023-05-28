using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMoveController : MonoBehaviour
{

    Rigidbody2D rb;

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
    [SerializeField] private float _moveDuration = 0.07f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }


    private void Start()
    {
        InputEvents.moveRightEvent.AddListener(OnMoveRightEvent);
        InputEvents.moveLeftEvent.AddListener(OnMoveLeftEvent);
        InputEvents.moveForwardEvent.AddListener(OnMoveForwardEvent);
        InputEvents.moveBackwardEvent.AddListener(OnMoveBackwardEvent);
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
        if (!IsMoving)
        {
            StartCoroutine(Move(transform.position,transform.position + Vector3.right));
        }
    }

    private void OnMoveLeftEvent()
    {
        if (!IsMoving)
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.left));
        }
    }

    private void OnMoveForwardEvent()
    {
        if (!IsMoving)
        {
            StartCoroutine(Move(transform.position, transform.position + Vector3.up));
        }
    }

    private void OnMoveBackwardEvent()
    {
        if (!IsMoving)
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
            transform.position = Vector3.Lerp(startPosition, targetPosition, elapsedTime/_moveDuration);
            elapsedTime+= Time.deltaTime;
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



}
