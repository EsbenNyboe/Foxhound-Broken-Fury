using System.Collections;
using UnityEngine;

namespace BrokenFury.Test
{
    public class SelectedTile : State
    {
        public SelectedTile(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            Debug.Log("SelectedTile State Active.");

            BattleSystem.BuildingInterface.SetActive(false);
            yield return new WaitForSeconds(0f);

            BattleSystem.TileInterface.SetTileInterface(BattleSystem.SelectedTile);            
            
        }

        public override IEnumerator ConstructBuilding(GameObject building)
        {
            Building buildScript = building.GetComponent<Building>();
            Debug.Log("Construct");
            BattleSystem.CurrentPlayer.ChangeOre(buildScript.Cost*(-1));
            //buildScript.NextTurnToProduce = BattleSystem.CurrentTurn + buildScript.TurnsToProduce;
            BattleSystem.Player();
            yield return new WaitForSeconds(0f);

        }
    }
}