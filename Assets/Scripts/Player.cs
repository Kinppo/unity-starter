using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PlayerState
{
    Idle,
    Run,
    Jump,
    Slide
}

public class Player : MonoBehaviour
{
    public static Player Instance { get; protected set; }
    private static readonly int Blend1 = Animator.StringToHash("Blend 1");
    private static readonly int Blend2 = Animator.StringToHash("Blend 2");
    private float para1, para2, progress;
    private bool isAnimating;
    public PlayerState playerState;
    public Animator animator;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        
    }

    private void Update()
    {
        if (isAnimating) SmoothAnimation();
    }

    public void ChangeAnimation(PlayerState state = PlayerState.Idle)
    {
        if (playerState == state) return;

        switch (state)
        {
            case PlayerState.Idle:
                para1 = 0;
                para2 = 0;
                break;
            case PlayerState.Run:
                para1 = 1;
                para2 = 1;
                break;
            case PlayerState.Jump:
                para1 = 1;
                para2 = -1;
                break;
            case PlayerState.Slide:
                para1 = -1;
                para2 = 1;
                break;
        }

        isAnimating = true;
        playerState = state;
    }
    
    private void SmoothAnimation()
    {
        var blend1 = Mathf.Lerp(animator.GetFloat(Blend1), para1, 10 * Time.deltaTime);
        var blend2 = Mathf.Lerp(animator.GetFloat(Blend2), para2, 10 * Time.deltaTime);

        animator.SetFloat(Blend1, blend1);
        animator.SetFloat(Blend2, blend2);

        if ((blend1 - para1 == 0) && (blend2 - para2 == 0))
            isAnimating = false;
    }
    
}
