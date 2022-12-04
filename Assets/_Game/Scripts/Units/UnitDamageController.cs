using System.Collections.Generic;
using UnityEngine;

namespace Game.Units
{
    public class UnitDamageController
    {
        private List<DamageSource> _damageSources;
        private List<DamageTarget> _damageTargets;

        public void Setup(UnitView view)
        {
            _damageSources = view.GetDamageSources();
            _damageTargets = view.GetDamageTargets();
        }

        public void AttackState(bool state)
        {
            foreach (var dmgSource in _damageSources)
            {
                dmgSource.DamageSourceState(state);
            }
        }
    }
}