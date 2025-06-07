using UnityEngine;
using System.Collections;

public class blue_tower : MonoBehaviour
{
    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            StartCoroutine(PlayExplosionThenDestroyed());
        }
    }

    IEnumerator PlayExplosionThenDestroyed()
    {
        animator.Play("explosion");

        // Wait for the explosion animation to finish
        // Using AnimatorStateInfo to get length dynamically
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until "explosion" state starts playing
        while (!state.IsName("explosion"))
        {
            yield return null;
            state = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait for animation length
        yield return new WaitForSeconds(state.length);

        animator.Play("blue_destroyed");
    }
}
