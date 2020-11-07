using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrokenFury.Test {
    public class HUD : MonoBehaviour
    {

        [SerializeField] private Text currentPlayer;
        [SerializeField] private Text turn;
        [SerializeField] private Text population;
        [SerializeField] private Text ore;
        [SerializeField] private Text fuel;
        [SerializeField] private Text food;


        public void SetCurrentPlayer(Player player)
        {
            currentPlayer.text = player.name;
            UpdatePopText(player);
            UpdateOreText(player);
            UpdateFuelText(player);
            UpdateFoodText(player);
        }

        public void UpdateTurnText(int t)
        {
            turn.text = t.ToString();
        }

        public void UpdatePopText(Player player)
        {
            population.text = "Pop: " + player.Population.ToString();
        }

        public void UpdateOreText(Player player)
        {
            ore.text = "Ore: " + player.Ore.ToString();
        }

        public void UpdateFuelText(Player player)
        {
            fuel.text = "Fuel: " + player.Fuel.ToString();
        }

        public void UpdateFoodText(Player player)
        {
            food.text = "Food: " + player.Food.ToString();
        }
    }
}
