using UnityEngine;

public class AnimationController : MonoBehaviour {

    private Animator _animator;

    private Vector3 previous;
    private float velocity;

    public void Init()
    {
        _animator = GetComponent<Animator>();
    }

    private void Update()
    {
        velocity = ((transform.position - previous).magnitude) / Time.deltaTime;
        previous = transform.position;
        _animator.SetFloat("Speed", velocity);
    }
}