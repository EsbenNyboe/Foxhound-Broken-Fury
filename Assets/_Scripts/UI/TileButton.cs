using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class TileButton : MonoBehaviour
    {
        private GameObject lot; // Where to build
        public GameObject panel;
        public GameObject button;

        public void Setup(GameObject tile, int index)
        {
            // Prepare the hex tile buttons, change colours and stuff
            Tile tileScript = tile.GetComponent<Tile>();
            lot = tileScript.Lots[index];

            if (index == 0)
            GetComponent<Image>().color = tile.GetComponentInChildren<SpriteRenderer>().color;

            GetComponentInChildren<Text>().text = lot.name;      
            GetComponent<Button>().onClick.AddListener(SetBuildMenu);
        }

        void SetBuildMenu()
        {
            // Create and populate the build panel with all available buildings.
            ClearPanel();

            LotManager building = lot.GetComponent<LotManager>();
            panel.SetActive(true);
            foreach(GameObject availBuild in building.AvailableBuildings)
            {
                if (availBuild.GetComponent<Building>().Cost > GameManager.instance.BattleSystem.CurrentPlayer.Ore) // TODO: Make smarter
                    continue;
                // Make a button for each available building
                GameObject newButton = Instantiate(button, Vector3.zero, Quaternion.identity, panel.transform)as GameObject;
                string buttonText = string.Format("{0}\n Cost: {1}", availBuild.name, availBuild.GetComponent<Building>().Cost);
                newButton.GetComponentInChildren<Text>().text = buttonText;

                void a() => lot.GetComponent<LotManager>().SetCurrentBuilding(availBuild);
                newButton.GetComponent<Button>().onClick.AddListener(a);
                newButton.GetComponent<Button>().onClick.AddListener(RemoveBuildMenu);
            }
        }

        void RemoveBuildMenu()
        {
            // Hide all tile UI elements
            ClearPanel();
            GameManager.instance.BattleSystem.TileInterface.HideUI();
            panel.SetActive(false);
        }

        void ClearPanel()
        {
            // Clear the panel, remove all listeners, destroy all buttons.
            foreach (Transform child in panel.transform)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
        }
    }
}
