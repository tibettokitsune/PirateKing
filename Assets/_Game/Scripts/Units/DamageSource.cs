using System;
using System.Collections.Generic;
using Sirenix.OdinInspector;
using UnityEngine;
using Zenject;

namespace Game.Units
{
    public class DamageSource : MonoBehaviour
    {
        [SerializeField, ReadOnly]private bool _isActive = false;
        private List<IDamaged> _tempDamageTargets;
        private int _teamID;
        public void DamageSourceState(bool state)
        {
            _isActive = state;
            if (state)
            {
                _tempDamageTargets = new List<IDamaged>();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(!_isActive) return;
            
            var dmg = other.GetComponent<IDamaged>();
            if(dmg == null) return;
            
            if (dmg.TeamID == _teamID) return;
            ;
            dmg?.GetDamage();
            _tempDamageTargets.Add(dmg);
        }
        
        public void Setup(int teamID)
        {
            _teamID = teamID;
        }
    }
}