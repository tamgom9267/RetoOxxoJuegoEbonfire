using UnityEngine;

public class LabPlayerControl : MonoBehaviour
{
    public float moveSpeed;
    public Rigidbody2D rig;
    public SpriteRenderer sr;
    Animator animatorController;

    void Start()
    {
        animatorController = GetComponent<Animator>();
    }

    void Update()
    {
        if (rig.linearVelocity.x > 0)
        {
            sr.flipX = false;
        }
        else if (rig.linearVelocity.x < 0)
        {
            sr.flipX = true;
        }
    }

    private void FixedUpdate()
    {
        float xInput = Input.GetAxis("Horizontal");
        float yInput = Input.GetAxis("Vertical");

        rig.linearVelocity = new Vector2(xInput * moveSpeed, yInput * moveSpeed);

        if (xInput != 0 || yInput != 0)
        {
            UpdateAnimation(PlayerAnimation.walk);
        }
        else
        {
            UpdateAnimation(PlayerAnimation.idle);
        }
    }

    public enum PlayerAnimation
    {
        idle, walk
    }

    void UpdateAnimation(PlayerAnimation nameAnimation)
    {
        switch (nameAnimation)
        {
            case PlayerAnimation.idle:
                animatorController.SetBool("IsWalking", false);
                break;
            case PlayerAnimation.walk:
                animatorController.SetBool("IsWalking", true);
                break;
        }
    }
}
