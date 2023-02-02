using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroHoveringState : HeroBaseState
{
    private readonly int HoveringHash = Animator.StringToHash("Hovering");
    private const float CrossFadeDuration = 0.1f;

    private float remainingHoverTime;

    public HeroHoveringState(HeroStateMachine stateMachine) : base(stateMachine) {}

    public override void Enter() 
    {
        stateMachine.InputReader.JumpEvent += OnJump;
        stateMachine.InputReader.DashEvent += OnDash;

        remainingHoverTime = stateMachine.HoverDuration;
        
        stateMachine.Animator.CrossFadeInFixedTime(HoveringHash, CrossFadeDuration);
    }

    public override void Tick(float deltaTime) 
    {
        remainingHoverTime -= deltaTime;

        if (remainingHoverTime <= 0f || !stateMachine.InputReader.HoverButtonDown) {
            if (stateMachine.Controller.isGrounded) {
                stateMachine.SwitchState(new HeroFreeLookState(stateMachine));
            } else {
                stateMachine.SwitchState(new HeroFallingState(stateMachine));
            }
        }
    }

    public override void Exit() 
    {
        stateMachine.InputReader.JumpEvent -= OnJump;
        stateMachine.InputReader.DashEvent -= OnDash;
    }

    private void OnJump()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Jump")) {
            stateMachine.SwitchState(new HeroJumpingState(stateMachine));
        }
    }

    private void OnDash()
    {
        if (stateMachine.AbilityTracker.TryAddAbility("Dash")) {
            stateMachine.SwitchState(new HeroDashingState(stateMachine, Vector2.up));
        }
    }
}
