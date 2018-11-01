using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AnimationController : MonoBehaviour {

    private NavMeshAgent _navMeshAgent;
    private Animator _animator;

    private Vector3 previous;
    private float velocity;

    public void Init()
    {
        _navMeshAgent = GetComponent<NavMeshAgent>();
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        _animator.SetFloat("Speed", velocity);
    }
}
