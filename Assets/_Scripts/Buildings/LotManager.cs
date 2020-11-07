using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class LotManager : MonoBehaviour
    {
        public List<GameObject> AvailableBuildings; // List of all available buildings for this lot
        private GameObject currentBuilding ; // The curently consturcted building on this lot

        public void SetCurrentBuilding(GameObject c)
        {
            // Construct the building at the desired location.
            currentBuilding = c;
            gameObject.name = currentBuilding.name;

            SetOwner(GameManager.instance.BattleSystem.CurrentPlayer);

            if (transform.childCount>0)
                Destroy(transform.GetChild(0).gameObject);
            GameObject tmp = Instantiate(currentBuilding, transform.position, Quaternion.identity, transform);
            tmp.name = gameObject.name;
            MeshRenderer[] tmpList = tmp.GetComponentsInChildren<MeshRenderer>();

            Player owner = GetOwner();

            tmp.GetComponent<Building>().Owner = owner;
            foreach(MeshRenderer mr in tmpList)
            {
                mr.material.color = owner.Color;
            }
            GameManager.instance.BattleSystem.OnConstructBuilding(currentBuilding);
        }

        public void SetOwner(Player player)
        {
            GetComponentInParent<Tile>().SetOwner(player);
            player.Tiles.Add(this.GetComponentInParent<Tile>());
        }

        public Player GetOwner()
        {
            Player owner = GetComponentInParent<Tile>().Owner;
            return owner;
        }

    }
}
