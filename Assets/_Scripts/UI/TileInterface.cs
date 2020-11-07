using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class TileInterface : MonoBehaviour
    {        
        public void SetTileInterface(GameObject tile)
        {
            // Show the hex buttons for the selected tile.
            this.gameObject.SetActive(true);
            int index = 0;
            foreach (Transform child in transform)
            {
                if(child.GetComponent<TileButton>()!=null)
                    child.GetComponent<TileButton>().Setup(tile, index);
                ++index;
            }
            SetOwnerText(tile);
        }

        public void HideUI()
        {           
            this.gameObject.SetActive(false);
        }

        void SetOwnerText(GameObject tile)
        {
            Player owner;
            Text text = transform.GetChild(transform.childCount - 1).GetComponent<Text>();
            if (tile.GetComponent<Tile>().Owner != null)
            {
                owner = tile.GetComponent<Tile>().Owner;
                text.text = owner.name;
            }
            else
            {
                text.text = "Not Claimed!";
                return;
            }
            
        }
    }
}