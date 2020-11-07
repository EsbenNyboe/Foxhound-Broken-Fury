using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class Tile : MonoBehaviour
    {
        public Vector2 GridID { get; set; } // Tile ID in the world grid.
        public List<GameObject> Neighbours { get; set; } // up to 6 tiles that share an edge with this one.
        public Vector3 Position { get; set; } // Position of this tile in world space.
        public float Elevation { get; set; } // The elevation of this tile.
        public Player Owner { get; set; } // Current owner of this tile.
        public List<GameObject> Lots { get; set; } // 7 possible locations to place buildings.

        private void Start()
        {
            Lots = new List<GameObject>();
            foreach(Transform child in transform)
            {
                Lots.Add(child.GetChild(0).gameObject);
            }
        }

        public void SetOwner(Player player)
        {
            Owner = player;
        }

        public void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (GameManager.instance.BattleSystem.CurrentPlayer != Owner && Owner !=null)
                return;
            GameManager.instance.BattleSystem.OnSelectTile(this.gameObject);
        }
    }
}
