using System.Collections;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Begin : State
    {
        public Begin(BattleSystem battleSystem) : base(battleSystem)
        {        
        }

        public override IEnumerator Start()
        {
            Debug.Log("Begin State Active.");
            BattleSystem.Interface.SetCurrentPlayer(BattleSystem.CurrentPlayer);
            BattleSystem.Interface.UpdateTurnText(BattleSystem.CurrentTurn);

            yield return new WaitForSeconds(1f);
            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }
    }
}