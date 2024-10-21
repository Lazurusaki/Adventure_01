using UnityEngine;
using UnityEngine.AI;

namespace ADV_11
{
    public class NavPointDisplayer : MonoBehaviour
    {
        [SerializeField] private NavMeshAgent _agent;
        [SerializeField] private ParticleSystem _effectPrefab;

        private ParticleSystem _effect;

        private void Awake()
        {
            if (_effectPrefab == null)
                throw new System.NullReferenceException("Effect Prefab is not set");
        }

        private void Update()
        {
            if (_agent.hasPath)
            {
                if (_effect == null)
                    _effect = StartDisplaying(_agent.destination);
                else if (_agent.destination != _effect.transform.position)
                    _effect.transform.position = _agent.destination;
            }
            else if (_agent.hasPath == false && _effect != null)
                StopDisplaying();
        }

        private ParticleSystem StartDisplaying(Vector3 position)
        {
            if (_effectPrefab == null)
                return null;

            return Instantiate(_effectPrefab, position, Quaternion.identity);
        }

        private void StopDisplaying()
        {
            if (_effect == null)
                return;

            Destroy(_effect.gameObject);
        }
    }
}
