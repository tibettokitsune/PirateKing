using GoodCat.Fsm;
using UnityEngine;

namespace Game.Units.UnitStates
{
    
    public class MovementState : State
    {
        private Vector2 _movementVector;
        private UnitController _currentUnitController;
        private UnitController _targetUnitController;
        private CharacterController _characterController;
        private UnitView _unitView;
        private UnitData _unitData;

        public MovementState(CharacterController characterController, UnitData unitData)
        {
            _characterController = characterController;
            _unitData = unitData;
        }

        public void UpdateTargets(UnitController unit, UnitController target)
        {
            _currentUnitController = unit;
            _targetUnitController = target;
        }

        public void UpdateMovementData(Vector2 movementVector) => _movementVector = movementVector;

        public void UpdateView(UnitView view)
        {
            _unitView = view;
        }
        
        protected override void OnEnable()
        {
            
        }

        protected override bool OnUpdate()
        {
            var forwardVector = _targetUnitController.GetTransformTarget().position - _currentUnitController.GetTransformTarget().position;
            var rightVector = Quaternion.AngleAxis(90, Vector3.up) * forwardVector;
            
            _characterController.Move(forwardVector.normalized * _movementVector.y * _unitData.MovementSpeed);
            _characterController.Move(rightVector.normalized * _movementVector.x * _unitData.MovementSpeed);
            _characterController.Move(Physics.gravity);
            
            forwardVector.y = 0;
            
            var rootRotation = Quaternion.LookRotation(forwardVector);
            _unitView.UpdateRotationData(rootRotation);
            _unitView.SimpleMovement(_movementVector);
            return true;
        }

        protected override void OnDisable()
        {
            
        }
    }
}