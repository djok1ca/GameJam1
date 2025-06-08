using UnityEngine;
using System.Collections;

public class blue_warrior : MonoBehaviour
{
    private Animator animator;
    public int mode=0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V) || mode==1)
        {
            animator.Play("fight");
        }
        if (Input.GetKeyDown(KeyCode.C) || mode==0)
        {
            animator.Play("run");
        }
        if (Input.GetKeyDown(KeyCode.X))
        {
            animator.Play("idle");
        }
        if (Input.GetKeyDown(KeyCode.Z))
        {
            StartCoroutine(PlayDeadAndDestroy());
        }
    }

    IEnumerator PlayDeadAndDestroy()
    {
        animator.Play("dead");

        // Wait until the "dead" animation state starts playing
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        while (!state.IsName("dead"))
        {
            yield return null;
            state = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait for the animation length
        yield return new WaitForSeconds(state.length);

        // Destroy or disable the object after animation finishes
        Destroy(gameObject);
        // Or: gameObject.SetActive(false);
    }
}
