using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Game.Units
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField, ReadOnly]private bool _isActive = false;

        public void DamageSourceState(bool state) => _isActive = state;

        private void OnTriggerEnter(Collider other)
        {
            if(!_isActive) return;
            
            var dmg = other.GetComponent<IDamaged>();
            dmg?.GetDamage();
        }
    }
}