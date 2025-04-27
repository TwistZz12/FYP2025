using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public float moveSpeed = 5f;
    public Animator animator;
    public bool isOnStairs = false;

    void Update()
    {
        if (isOnStairs)
        {
            float h = Input.GetAxis("Horizontal");
            Vector3 move = new Vector3(h * moveSpeed * Time.deltaTime, 0, 0);
            transform.Translate(move);

            if (animator != null)
            {
                animator.SetBool("isOnStairs", true);
                animator.SetFloat("Speed", Mathf.Abs(h));
            }

            return;
        }
    }

} 