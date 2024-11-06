using System;
using UnityEngine;
using Object = UnityEngine.Object;

namespace ADV_13
{
    public class CharacterFabric
    {
        private readonly Character _playerPrefab;
        private readonly Character _enemyPrefab;

        public event Action<Character> CharacterSpawned;

        public CharacterFabric(Character playerPrefab, Character enemyPrefab)
        {
            _playerPrefab = playerPrefab;
            _enemyPrefab = enemyPrefab;
        }

        public Character SpawnCharacter(CharacterTypes characterType, Vector3 position)
        {
            Character characterPrefab = null;

            switch (characterType)
            {
                case CharacterTypes.Player:
                    characterPrefab = _playerPrefab;
                    break;
                case CharacterTypes.Enemy:
                    characterPrefab = _enemyPrefab;
                    break;
            }

            Character character = Object.Instantiate(characterPrefab, position, Quaternion.identity, null);
            character.Initialize();
            CharacterSpawned?.Invoke(character);

            return character;
        }
    }
}