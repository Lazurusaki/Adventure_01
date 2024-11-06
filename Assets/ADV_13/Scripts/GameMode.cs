using System;

namespace ADV_13
{
    public class GameMode
    {
        //private readonly ICondition _winCondition;
        //private readonly ICondition _looseCondition;
        //private readonly KillCounter _killCounter;

        public event Action Win;
        public event Action Loose;

        public GameMode(ICondition winCondition, ICondition looseCondition)
        {
            winCondition.Completed += () => Win?.Invoke();
            looseCondition.Completed += () => Loose?.Invoke();
        }
        
        // public GameMode(Character character, ObservableList<Character> enemies, KillCounter killCounter,
        // WinConditions winConditionName, LooseConditions looseConditionName)
        // {
        //     switch (winConditionName)
        //     {
        //         case WinConditions.TimeSurvival:
        //             _winCondition = new TimeSurvival(character);
        //             break;
        //         case WinConditions.Elimination:
        //             _winCondition = new Elemination(killCounter);
        //             break;
        //     }
        //
        //     switch (looseConditionName)
        //     {
        //         case LooseConditions.Died:
        //             _looseCondition = new Died(character);
        //             break;
        //         case LooseConditions.EnemyOverload:
        //             _looseCondition = new EnemyOverload(enemies);
        //             break;
        //     }
        //
        //     _winCondition.Completed += OnWin;
        //     _looseCondition.Completed += OnLoose;
        // }
    }
}