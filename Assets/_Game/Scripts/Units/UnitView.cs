using System;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public enum UnitViewVariant
    {
        TestDummy
    }
    
    public class UnitView : MonoBehaviour
    {
        [Inject] private Animator _animator;
        
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int MovementBoost = Animator.StringToHash("IsMovementBoost");
        private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");
        private static readonly int IsEvade = Animator.StringToHash("IsEvade");
        private static readonly int IsLanded = Animator.StringToHash("IsLanded");

        public void UpdateRotationData(Quaternion rootRotation)
        {
            transform.rotation = rootRotation;
        }

        public void UpdateAnimationData(Vector3 movement, bool isLanded, bool isEvade, bool isMovementBoost, 
            bool isCrouch)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, isMovementBoost);
            _animator.SetBool(IsCrouch, isCrouch);
            _animator.SetBool(IsEvade, isEvade);
        }
        
        public void SimpleMovement(Vector3 movement)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, false);
            _animator.SetBool(IsCrouch, false);
            _animator.SetBool(IsEvade, false);
            _animator.SetBool(IsLanded, true);
        }
        
        public void JumpMovement(Vector3 movement)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, false);
            _animator.SetBool(IsCrouch, false);
            _animator.SetBool(IsEvade, false);
            _animator.SetBool(IsLanded, false);
        }
        
        public void Crouching(Vector3 movement)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, false);
            _animator.SetBool(IsCrouch, true);
            _animator.SetBool(IsEvade, false);
            _animator.SetBool(IsLanded, true);
        }
        
        public void BoostMovement(Vector3 movement)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, true);
            _animator.SetBool(IsCrouch, false);
            _animator.SetBool(IsEvade, false);
            _animator.SetBool(IsLanded, true);
        }
        
        public void EvadeMovement(Vector3 movement)
        {
            _animator.SetFloat(Vertical, movement.y);
            _animator.SetFloat(Horizontal, movement.x);
            _animator.SetBool(MovementBoost, false);
            _animator.SetBool(IsCrouch, false);
            _animator.SetBool(IsEvade, true);
            _animator.SetBool(IsLanded, true);
        }
    }
}