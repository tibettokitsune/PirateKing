using UnityEngine;

namespace Game.Infrastructure
{
    public interface IInput
    {
        float Horizontal();
        float Vertical();
        float IsJump();
        float IsEvade();
        float IsAttack();

        float IsCrouch();
        float IsMovementBoost();
    }
    
    public class PlayerInput : IInput
    { 
        public float Horizontal() => Input.GetAxis("Horizontal");

        public float Vertical() => Input.GetAxis("Vertical");

        public float IsJump() => Input.GetAxis("Jump");

        public float IsEvade() => Input.GetAxis("Evade");

        public float IsAttack() => Input.GetAxis("Fire1");
        public float IsCrouch() => Input.GetAxis("Crouch");
        public float IsMovementBoost() => Input.GetAxis($"MovementBoost");
    }
}