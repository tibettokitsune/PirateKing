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

        public void UpdateRotationData(Quaternion rootRotation)
        {
            transform.rotation = rootRotation;
        }

        public void UpdateAnimationData(float horizontal, float vertical, bool isJump, bool isEvade)
        {
            _animator.SetFloat(Vertical, vertical);
            _animator.SetFloat(Horizontal, horizontal);
        }
    }
}