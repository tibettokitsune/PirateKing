using System;
using System.Collections.Generic;
using DG.Tweening;
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
        [Inject] public readonly AnimatorObserver AnimatorObserver;
        [Inject] private List<DamageSource> _damageSources;
        [Inject] private List<DamageTarget> _damageTargets;
        
        private static readonly int Vertical = Animator.StringToHash("Vertical");
        private static readonly int Horizontal = Animator.StringToHash("Horizontal");
        private static readonly int MovementBoost = Animator.StringToHash("IsMovementBoost");
        private static readonly int IsCrouch = Animator.StringToHash("IsCrouch");
        private static readonly int IsEvade = Animator.StringToHash("IsEvade");
        private static readonly int IsLanded = Animator.StringToHash("IsLanded");
        private static readonly int Attack = Animator.StringToHash("Attack");
        private static readonly int VerticalAttack = Animator.StringToHash("VerticalAttack");
        private static readonly int HorizontalAttack = Animator.StringToHash("HorizontalAttack");
        private static readonly int Falling = Animator.StringToHash("Falling");

        private Tween _layersBlendTween;
        public void UpdateRotationData(Quaternion rootRotation)
        {
            transform.rotation = rootRotation;
        }

        public void AttackAnimation(Vector2 direction)
        {
            _animator.SetFloat(HorizontalAttack, direction.x);
            _animator.SetFloat(VerticalAttack, direction.y);
            _animator.SetBool(Attack, true);
            _animator.SetLayerWeight(1, 1f);
            AnimatorObserver.OnAnimationEnd.Take(1).Subscribe(_ =>
            {
                _animator.SetLayerWeight(1, 0f);
                _animator.SetBool(Attack, false);
            });
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

        public List<DamageSource> GetDamageSources() => _damageSources;

        public List<DamageTarget> GetDamageTargets() => _damageTargets;

        
        
        public void DamageEffect()
        {
            _layersBlendTween.Kill();
            _layersBlendTween = DOVirtual.Float(0, 1, 1f, v =>
            {
                
                _animator.SetLayerWeight(2, v);
            });
            // _animator.SetLayerWeight(2, 1f);
            _animator.SetTrigger(Falling);
            AnimatorObserver.OnAnimationEnd.Take(1).Subscribe(_ =>
            {
                _layersBlendTween.Kill();
                _layersBlendTween = DOVirtual.Float(1, 0, 1f, v =>
                {
                    _animator.SetLayerWeight(2, v);
                });
                // _animator.SetLayerWeight(2, 0f);
            });
        }
    }
}