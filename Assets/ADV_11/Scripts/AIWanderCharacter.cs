using UnityEngine;

namespace ADV_11
{
    public class AIWanderCharacter : IController
    {
        private CharacterController _characterController;
        private float _wanderRadius;

        public AIWanderCharacter(CharacterController characterController, float wanderRadius)
        {
            _characterController = characterController;
            _wanderRadius = wanderRadius;
        }

        public void Update()
        {
            if (_characterController != null) 
                if (_characterController.IsTargetReached)
                    if (TryFindPosition(out var position))
                        _characterController.MoveTo(position);
        }

        private bool TryFindPosition(out Vector3 position)
        {
            float tryCount = 10;
            position = Vector3.zero;

            for (int i = 0; i <= tryCount; i++)
            {
                Vector3 randomPoint = Random.insideUnitSphere * _wanderRadius + _characterController.transform.position;

                if (UnityEngine.AI.NavMesh.SamplePosition(randomPoint, out UnityEngine.AI.NavMeshHit hit, _wanderRadius, UnityEngine.AI.NavMesh.AllAreas))
                {
                    position = hit.position;
                    return true;
                }
            }

            return false;
        }
    }
}
