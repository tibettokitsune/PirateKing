using Zenject;

namespace Game.Units
{
    public class UnitController
    {
        private UnitData _unitData;
        
        public class Factory : PlaceholderFactory<UnitController>
        {
        }
    }
}