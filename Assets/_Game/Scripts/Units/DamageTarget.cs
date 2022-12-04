using UnityEngine;

namespace Game.Units
{
    public enum BodyPart
    {
        Head,
        LeftHand,
        RightHand,
        Body,
        LeftLeg,
        RightLeg
    }

    public enum DamageState
    {
        NoDamage,
        MiddleDamage,
        HighDamage,
        FullDamage
    }

    public interface IDamaged
    {
        public void GetDamage();
    }
    
    public class DamageTarget : MonoBehaviour, IDamaged
    {
        [SerializeField] private BodyPart bodyPart;
        [SerializeField] private DamageState damageState;
        public void GetDamage()
        {
            Debug.Log("Damage");
        }
    }
}