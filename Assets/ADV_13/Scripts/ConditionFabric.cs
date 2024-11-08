using System;
using UnityEngine;

namespace ADV_13
{
    public class ConditionFabric
    {
        private readonly MonoBehaviour _coroutineHost;
        private readonly CharacterHolder _characterHolder;
        private readonly KillCounter _killCounter;
        private readonly ObservableList<Character> _enemies;

        public ConditionFabric( MonoBehaviour coroutineHost, CharacterHolder characterHolder,
                                ObservableList<Character> enemies,
                                KillCounter killCounter)
        {
            _coroutineHost = coroutineHost;
            _characterHolder = characterHolder;
            _killCounter = killCounter;
            _enemies = enemies;
        }

        public ICondition CreateCondition(EndGameConditions conditionName)
        {
            ICondition condition;
            
            switch (conditionName)
            {
                case EndGameConditions.TimeSurvival:
                    condition =  new TimeSurvival(_coroutineHost);
                    break;
                case EndGameConditions.Elemination:
                    condition =  new Elemination(_killCounter);
                    break;
                case EndGameConditions.Died:
                    condition =  new Died(_characterHolder);
                    break;
                case EndGameConditions.EnemyOverload:
                    condition =  new EnemyOverload(_enemies);
                    break;
                default: throw new ArgumentException("Unknown condition.", nameof(conditionName));
            }

            return condition;
        }
    }
}