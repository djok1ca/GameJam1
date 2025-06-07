using UnityEngine;
using System.Collections;

public class red_tower : MonoBehaviour
{

    private Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q))
        {
            StartCoroutine(PlayExplosionThenDestroyed());
        }
    }

    IEnumerator PlayExplosionThenDestroyed()
    {
        animator.Play("red_tower_explosion");

        // Wait for the explosion animation to finish
        // Using AnimatorStateInfo to get length dynamically
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);

        // Wait until "explosion" state starts playing
        while (!state.IsName("red_tower_explosion"))
        {
            yield return null;
            state = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait for animation length
        yield return new WaitForSeconds(state.length);

        animator.Play("red_tower_destroyed");
    }
}
