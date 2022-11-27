using System;
using UnityEngine;

namespace Game.Units
{
    [RequireComponent(typeof(Collider)), RequireComponent(typeof(Rigidbody)), ]
    public class CharacterDamageElement : MonoBehaviour, IDamageElement
    {
        public DegreeOfDamage DegreeOfDamage { get; }
        [SerializeField] private Collider _collider;
        [SerializeField] private Rigidbody _rb;
        
        private void OnValidate()
        {
            _collider = GetComponent<Collider>();
            _rb = GetComponent<Rigidbody>();
        }

        public void GetDamage(DamageType damageType, float velocity, Vector3 sourcePoint)
        {
            throw new NotImplementedException();
        }
    }
}