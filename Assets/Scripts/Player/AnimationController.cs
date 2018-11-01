using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour {

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Vector3 previous;
    private float velocity;

    private void Start()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        Debug.Log(velocity);
        _animator.SetFloat("Speed", velocity);
    }
}
