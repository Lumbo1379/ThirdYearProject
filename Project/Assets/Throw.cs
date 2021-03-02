using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Throw : MonoBehaviour
{
    [SerializeField] private float _throwForce;
    [SerializeField] private Vector3 _target;
    [SerializeField] private Vector3 _targetOffset;
    [SerializeField] private float _delayBetweenThrows;
    [SerializeField] private ParticleSystem[] _catchBallVFX;

    private Vector3 _startPosition;
    private Rigidbody _rigidBody;

    private void Awake()
    {
        _startPosition = transform.position;
        _rigidBody = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (transform.position.y < -10.0f)
            InvokeThrow();
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.transform.tag == "Floor")
            InvokeThrow();
        else if (collision.transform.tag == "Hand")
            CatchBall();
    }

    private void CatchBall()
    {
        foreach (var ps in _catchBallVFX)
        {
            ps.Play();
        }
    }

    private void InvokeThrow()
    {
        Invoke("ThrowAtTarget", _delayBetweenThrows);
    }

    private void ThrowAtTarget()
    {
        CancelInvoke("ThrowAtTarget");

        transform.position = _startPosition;

        _rigidBody.velocity = Vector3.zero;

        var direction = _target + _targetOffset - transform.position;
        _rigidBody.AddForce(direction * _throwForce);
    }
}
