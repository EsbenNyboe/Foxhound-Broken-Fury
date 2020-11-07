using System.Collections;
using UnityEngine;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class PlayerTurn : State
    {
        public PlayerTurn(BattleSystem battleSystem) : base(battleSystem)
        {
        }

        public override IEnumerator Start()
        {
            // Hide all buttons, update hud to display info for current player.
            Debug.Log("PlayerTurn State Active. " + BattleSystem.CurrentPlayer.Name);

            BattleSystem.BuildingInterface.SetActive(false);
            BattleSystem.TileInterface.HideUI();
            if (BattleSystem.NewTurn)
            {
                Debug.Log("This is a new turn. Resources are being produced.");
                BattleSystem.NewTurn = false;

                foreach (Tile t in BattleSystem.CurrentPlayer.Tiles)
                {
                    Building build = t.GetComponentInChildren<Building>();
                    build.PanelButton = BattleSystem.AddToBuildingPanel(build);
                    build.UpdateButtonData();
                    if (BattleSystem.CurrentTurn > build.NextTurnToProduce)
                    {
                        Debug.Log("Producing from "+ build.gameObject.name);
                        build.AddToStorage();
                    }                    
                }
            }
            BattleSystem.Interface.SetCurrentPlayer(BattleSystem.CurrentPlayer);

            yield return new WaitForSeconds(1f);
        }

        public override IEnumerator ConsumeFood(int amount)
        {
            yield return new WaitForSeconds(0f);
        }

        public override IEnumerator Tile()
        {
            yield return new WaitForSeconds(0f);

            BattleSystem.SetState(new SelectedTile(BattleSystem));
        }

        public override IEnumerator Building()
        {
            yield return new WaitForSeconds(0f);

            BattleSystem.SetState(new SelectedBuilding(BattleSystem));
        }

    }
}