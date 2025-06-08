using UnityEngine;
using System.Collections;


public class red_warrior : MonoBehaviour
{
    private Animator animator;
    public int mode = 0;
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.U)|| mode==1)
        {
            animator.Play("red_fight");
        }
        if (Input.GetKeyDown(KeyCode.Y)|| mode==0)
        {
            animator.Play("red_run");
        }
        if (Input.GetKeyDown(KeyCode.T))
        {
            animator.Play("red_idle");
        }
        if (Input.GetKeyDown(KeyCode.I) || mode==2)
        {
            animator.Play("red_frozen");
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            StartCoroutine(PlayDeadAndDestroy());
        }
    }

    IEnumerator PlayDeadAndDestroy()
    {
        animator.Play("red_dead");

        // Wait until the "red_dead" animation state starts playing
        AnimatorStateInfo state = animator.GetCurrentAnimatorStateInfo(0);
        while (!state.IsName("red_dead"))
        {
            yield return null;
            state = animator.GetCurrentAnimatorStateInfo(0);
        }

        // Wait for the animation length
        yield return new WaitForSeconds(state.length);

        // Destroy or disable the object after animation finishes
        Destroy(gameObject);
        // Or use: gameObject.SetActive(false);
    }
}
