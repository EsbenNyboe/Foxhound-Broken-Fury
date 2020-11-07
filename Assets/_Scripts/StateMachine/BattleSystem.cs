using UnityEngine;
using System.Collections;
using UnityEngine.UI;

namespace BrokenFury.Test
{
    public class BattleSystem : StateMachine
    {
        [SerializeField] private HUD hud;
        [SerializeField] private Player player1; // make a List of players in the future
        [SerializeField] private Player player2;
        [SerializeField] private TileInterface tileInterface;
        [SerializeField] private GameObject buildingInterface;
        [SerializeField] private GameObject buildingPanel;
        [SerializeField] private GameObject button;

        public GameObject BuildingInterface => buildingInterface;
        public GameObject BuildingPanel => buildingPanel;
        public GameObject Button => button;
        public HUD Interface => hud;
        public Player Player1 => player1;
        public Player Player2 => player2; // make a list of players
        public TileInterface TileInterface => tileInterface;
        public Player CurrentPlayer { get; private set; }
        public int CurrentTurn { get; private set; }
        public GameObject SelectedTile { get; private set; }
        public GameObject SelectedBuilding { get; private set; }
        public bool NewTurn { get; set; }


    private void Start()
        {
            GameManager.instance.BattleSystem = this;
            player1.NewGame();
            player2.NewGame();
            CurrentPlayer = player1;
            SetState(new Begin(this));
        }

        public void OnNextButton()
        {
            NewTurn = true;
            ClearPanel();

            if (CurrentPlayer.Name == "Player1")
            {
                Debug.Log("Loading: Player2, please stand by.");
                CurrentPlayer = Player2;
            }else if (CurrentPlayer.Name == "Player2")
            {
                Debug.Log("Loading: Player1, please stand by.");
                CurrentPlayer = Player1;
                CurrentTurn++;
            }
            
            Interface.SetCurrentPlayer(CurrentPlayer);
            Interface.UpdateTurnText(CurrentTurn);

            Player();
        }

        public void Player()
        {
            StartCoroutine(State.NextPlayer());
        }

        public void OnConstructBuilding(GameObject build)
        {
            StartCoroutine(State.ConstructBuilding(build));
        }
        public void OnSelectTile(GameObject tile)
        {
            SelectedTile = tile;
            StartCoroutine(State.Tile());
        }

        public void OnSelectBuilding(GameObject build)
        {
            SelectedBuilding = build;
            StartCoroutine(State.Building());
        }

        public void AddWorker()
        {
            Building b = SelectedBuilding.GetComponent<Building>();
            
            if(CurrentPlayer.Population>0 && b.Workers < b.WorkerCapacity)
            {
                b.ChangeWorker(1);
            }
            Interface.UpdatePopText(CurrentPlayer);
        }

        public void RemoveWorker()
        {
            Building b = SelectedBuilding.GetComponent<Building>();
            
            if (b.Workers > 0)
            {
                b.ChangeWorker(-1);
            }
            Interface.UpdatePopText(CurrentPlayer);
        }

        public void CollectResources()
        {
            Building b = SelectedBuilding.GetComponent<Building>();

            if(b.Storage > 0)
            {
                b.CollectStorage();
            }

            UpdateResources();
        }

        public void CollectAll()
        {
            foreach (Tile t in CurrentPlayer.Tiles)
            {
                Building build = t.GetComponentInChildren<Building>();
                build.CollectStorage();
            }
            UpdateResources();
        }

        private void UpdateResources()
        {
            Interface.UpdateFoodText(CurrentPlayer);
            Interface.UpdateOreText(CurrentPlayer);
            Interface.UpdateFuelText(CurrentPlayer);
            Interface.UpdatePopText(CurrentPlayer);
        }

        public GameObject AddToBuildingPanel(Building build)
        {            
            GameObject newButton = Instantiate(Button, Vector3.zero, Quaternion.identity, buildingPanel.transform) as GameObject;
            newButton.GetComponentInChildren<Text>().text = build.GetData();
            void a() => OnSelectBuilding(build.gameObject);
            newButton.GetComponent<Button>().onClick.AddListener(a);

            return newButton;
        }

        void ClearPanel()
        {
            // Clear the panel, remove all listeners, destroy all buttons.
            foreach (Transform child in buildingPanel.transform)
            {
                child.GetComponent<Button>().onClick.RemoveAllListeners();
                Destroy(child.gameObject);
            }
        }

    }
}
