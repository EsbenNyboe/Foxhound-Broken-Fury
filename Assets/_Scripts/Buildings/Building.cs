using System.Collections;
using System.Collections.Concurrent;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class Building : Entity
    {
        [SerializeField] private int productionBase;
        [SerializeField] private float storageCapacity;
        [SerializeField] private float workerCapacity;
        [SerializeField] private int cost;
        [SerializeField] private int fuelConsumption;
        [SerializeField] private int collectionFuelCost;
        [SerializeField] private int turnsToProduce;

        public int ProductionBase => productionBase;
        public float StorageCapacity => storageCapacity;
        public float WorkerCapacity => workerCapacity;
        public int Cost => cost;
        public int FuelConsumption => fuelConsumption;
        public int CollectionFuelCost => collectionFuelCost;
        public int TurnsToProduce => turnsToProduce;

        public int Workers { get; private set; }
        public int Storage { get; private set; }
        public int ProductinoBonus { get; private set; }
        public int NextTurnToProduce { get; private set; }
        public GameObject PanelButton { get; set; }
        public Player Owner { get; set; }
        private int production;                   

        private void Start()
        {
            Workers = 0;            
            NextTurnToProduce = GameManager.instance.BattleSystem.CurrentTurn + turnsToProduce;
            GameManager.instance.BattleSystem.Interface.SetCurrentPlayer(Owner);
            PanelButton = GameManager.instance.BattleSystem.AddToBuildingPanel(this);
            CalculateBonus();
        }

        public void CalculateBonus()
        {
            List<GameObject> tileList = new List<GameObject>();
            tileList.Add(this.GetComponentInParent<Tile>().gameObject);
            tileList = FindBonus(tileList, 0);
            ProductinoBonus = tileList.Count - 1;
            SetProductionBonus(tileList.Count-1);
            foreach(GameObject t in tileList)
            {
                if (t == this)
                    continue;
                t.GetComponentInChildren<Building>().ProductinoBonus = ProductinoBonus;
                t.GetComponentInChildren<Building>().SetProductionBonus(ProductinoBonus);
            }
        }

        List<GameObject> FindBonus(List<GameObject> tileList, int index) 
        {
            Debug.Log("Found "+index.ToString()+ " valid adjacent buildings.");
            foreach (GameObject neighbourTile in tileList[index].GetComponent<Tile>().Neighbours)
            {
                Tile t = neighbourTile.GetComponent<Tile>();
                if (t.Lots[0].name == name && t.Owner == Owner && !tileList.Contains(neighbourTile))
                {
                    tileList.Add(neighbourTile);
                    FindBonus(tileList, tileList.Count - 1);
                }
            }
            return tileList;
        }

        public void SetProductionBonus(int bonus)
        {
            ProductinoBonus = bonus;
            UpdateButtonData();
        }

        public virtual void AddToStorage()
        {
            Debug.Log("Adding to Storage.");
            NextTurnToProduce = GameManager.instance.BattleSystem.CurrentTurn + turnsToProduce;

            production = (int)((ProductionBase + ((ProductionBase * ProductinoBonus) / 10f))*(Workers/workerCapacity));
            Debug.Log(Workers / workerCapacity);
            Debug.Log("Expected production " + production.ToString());
            if ((Storage + production) <= StorageCapacity && Owner.ConsumeFood(Workers) && Owner.ConsumeFuel((int)(FuelConsumption * (1 - ProductinoBonus / 10f))))
            {
                Storage += production;                
                Debug.Log(production.ToString() + " was added to storage." + Storage.ToString() + " / " + StorageCapacity.ToString());                
            }
            UpdateButtonData();
        }

        public void ChangeWorker(int i)
        {
            Owner.ChangePop(i*(-1));
            Workers += i;
            UpdateButtonData();
            Debug.Log("Current worker count in "+this.name+" is "+Workers.ToString()+" / "+workerCapacity.ToString());
        }

        public virtual void CollectStorage()
        {
            if(Owner.ConsumeFuel(5))
                Owner.ChangeOre(Storage);            
        }

        public void EmptyStorage()
        {
            Storage = 0;
            UpdateButtonData();
        }

        public void UpdateButtonData()
        {
            float grad = 1-Storage / storageCapacity;
            Color buttonColour = new Color(1, grad, grad);
            PanelButton.GetComponent<Image>().color = buttonColour;
            PanelButton.GetComponentInChildren<Text>().text = GetData();
        }

        public string GetData()
        {
            string tileName = GetComponentInParent<Tile>().gameObject.name;
            string data = string.Format("{0} {1}\n Storage: {2}/{3} Work: {4}/{5} Bonus: {6}", name, tileName, Storage, StorageCapacity, Workers, WorkerCapacity, ProductinoBonus);
            return data;
        }

        private void OnMouseDown()
        {
            if (EventSystem.current.IsPointerOverGameObject())
                return;
            if (GameManager.instance.BattleSystem.CurrentPlayer != Owner && Owner != null)
                return;
            
            GameManager.instance.BattleSystem.OnSelectBuilding(this.gameObject);          
        }
    }
}
