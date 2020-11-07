using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public abstract class State : MonoBehaviour
    {
        protected BattleSystem BattleSystem;

        public State(BattleSystem battleSystem)
        {
            BattleSystem = battleSystem;
        }

        public virtual IEnumerator Start()
        {
            yield break;
        }

        public virtual IEnumerator Unit()
        {
            yield break;
        }

        public virtual IEnumerator NextPlayer()
        {
            yield return new WaitForSeconds(0f);

            BattleSystem.SetState(new PlayerTurn(BattleSystem));
        }

        public virtual IEnumerator Tile()
        {
            yield break;
        }

        public virtual IEnumerator Building()
        {
            yield break;
        }

        public virtual IEnumerator ConstructBuilding(GameObject building)
        {
            yield break;
        }

        public virtual IEnumerator ConsumeFood(int amount)
        {
            yield break;
        }
    }
}
