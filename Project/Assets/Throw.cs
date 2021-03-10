using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    [SerializeField] private Vector3[] _targets;
    [SerializeField] private Vector3 _targetOffset;
    [SerializeField] private float _delayBetweenThrows;
    [SerializeField] private ParticleSystem[] _catchBallVFX;
    [SerializeField] private Countdown _countdown;
    [SerializeField] private FirebaseController _firebaseController;
    [SerializeField] private Transform _leftHand;
    [SerializeField] private Transform _rightHand;

    public bool HasStartedThrowing { get; set; }

    private Vector3 _startPosition;
    private Rigidbody _rigidBody;
    private Vector3 _currentTarget;
    private int _nextTargetIndex;
    private bool _ignoreCollisions;
    private Score[] _scores;
    private bool _resultsSent;
    private float _closestToHand;

    private void Awake()
    {
        _startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();

        _currentTarget = _targets[0];
        _nextTargetIndex = 1;

        HasStartedThrowing = false;
        _ignoreCollisions = true;
        _resultsSent = false;

        _closestToHand = float.MaxValue;

        _scores = new Score[5];
    }

    private void Update()
    {
        if (transform.position.y < -10.0f)
            GetNextTarget();

        if (HasStartedThrowing && !_resultsSent)
            SetClosest();
    }

    private void SetClosest()
    {
        float left = Vector3.Distance(transform.position, _leftHand.position);
        float right = Vector3.Distance(transform.position, _rightHand.position);

        float closestThisFrame = left < right ? left : right;

        if (closestThisFrame < _closestToHand)
            _closestToHand = closestThisFrame;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (HasStartedThrowing && !_ignoreCollisions && !_resultsSent)
        {
            if (collision.transform.tag == "Floor")
            {
                AddScore(_closestToHand);

                GetNextTarget();
            }
            else if (collision.transform.tag == "Hand")
                CatchBall(collision);
        }
    }

    private void CatchBall(Collision collision)
    {
        foreach (var ps in _catchBallVFX)
        {
            ps.Play();
        }

        float distance = Vector3.Distance(transform.position, collision.transform.position);
        AddScore(distance);

        GetNextTarget();
    }

    private void AddScore(float value)
    {
        Debug.Log($"Score {_nextTargetIndex - 1}: {value}");

        var score = new Score
        {
            doubleValue = value
        };

        _scores[_nextTargetIndex - 1] = score;
    }

    private void InvokeThrow()
    {
        _ignoreCollisions = true;
        _countdown.StartCountdown(_delayBetweenThrows);
        Invoke("ThrowAtTarget", _delayBetweenThrows);
    }

    private void ThrowAtTarget()
    {
        _ignoreCollisions = false;
        _rigidBody.isKinematic = false;
        _rigidBody.velocity = Vector3.zero;

        var direction = _currentTarget + _targetOffset - transform.position;
        _rigidBody.AddForce(direction * _throwForce);
    }

    private void GetNextTarget()
    {
        if (_resultsSent) return;

        if (_nextTargetIndex >= _targets.Length)
        {
            _firebaseController.AddUser(_scores);
            _resultsSent = true;
            return;
        }

        _closestToHand = float.MaxValue;

        _rigidBody.isKinematic = true;
        transform.position = _startPosition;

        _currentTarget = _targets[_nextTargetIndex];
        _nextTargetIndex++;

        InvokeThrow();
    }

    public void StartThrowing()
    {
        HasStartedThrowing = true;

        InvokeThrow();
    }
}
