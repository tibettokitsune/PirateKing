using UnityEngine;

namespace Game.Infrastructure
{
    public interface IInput
    {
        float Horizontal();
        float Vertical();
        float IsJump();
        float IsEvade();
        bool IsAttack();

        float HorizontalAttack();
        float VerticalAttack();

        float IsCrouch();
        float IsMovementBoost();
    }
    
    public class PlayerInput : IInput
    { 
        public float Horizontal() => Input.GetAxis("Horizontal");

        public float Vertical() => Input.GetAxis("Vertical");

        public float IsJump() => Input.GetAxis("Jump");

        public float IsEvade() => Input.GetAxis("Evade");

        public bool IsAttack() => Mathf.Abs(HorizontalAttack()) > 0  || Mathf.Abs(VerticalAttack()) > 0;
        public float HorizontalAttack() => Input.GetAxis("HorizontalAttack");

        public float VerticalAttack() => Input.GetAxis("VerticalAttack");

        public float IsCrouch() => Input.GetAxis("Crouch");
        public float IsMovementBoost() => Input.GetAxis($"MovementBoost");
    }
}