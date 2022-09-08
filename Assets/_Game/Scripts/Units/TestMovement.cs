using System;
using UnityEngine;

namespace Game.Units
{
    public class TestMovement : MonoBehaviour
    {
        [SerializeField] private AnimationCurve[] movementCurves;

        [SerializeField] private float speed;
        [SerializeField] private float radius;
        private float _timer;
        private void FixedUpdate()
        {
            transform.position = new Vector3(Mathf.Sin(_timer),
                0,
                Mathf.Cos(_timer)) * radius;

            _timer += Time.deltaTime * speed;

            if (_timer >= 2 * Mathf.PI)
                _timer -= 2 * Mathf.PI;


        }
    }
}