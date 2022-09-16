using GoodCat.Fsm;
using UnityEngine;

namespace Game.Units.UnitStates
{
    public class JumpState : State
    {
        private Vector2 _movementVector;
        private UnitController _currentUnitController;
        private UnitController _targetUnitController;
        private UnitView _unitView;
        
        private readonly CharacterController _characterController;
        private readonly UnitData _unitData;

        private Vector3 _jumpVelocity;
        private float _jumpTimer;
        private const float StartDelay = 0.25f;
        
        public JumpState(CharacterController characterController, UnitData unitData)
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

        public bool IsReadyToSwitch() => _jumpTimer > StartDelay;
        
        protected override void OnEnable()
        {
            _jumpVelocity = Vector3.up * _unitData.JumpSpeed - Physics.gravity;
            _jumpTimer = 0f;
            _characterController.Move(_jumpVelocity * Time.deltaTime);
        }

        protected override bool OnUpdate()
        {
            _jumpTimer += Time.deltaTime;
            var forwardVector = _targetUnitController.GetTransformTarget().position - _currentUnitController.GetTransformTarget().position;
            var rightVector = Quaternion.AngleAxis(90, Vector3.up) * forwardVector;
            _characterController.Move(_jumpVelocity * Time.deltaTime);
            _characterController.Move(rightVector.normalized * _movementVector.x * _unitData.MovementSpeed);
            forwardVector.y = 0;
            _jumpVelocity.y += Time.deltaTime * Physics.gravity.y;
            var rootRotation = Quaternion.LookRotation(forwardVector);
            _unitView.UpdateRotationData(rootRotation);
            _unitView.JumpMovement(_movementVector);
            return true;
        }

        protected override void OnDisable()
        {
        }
    }
}