using UnityEngine;
using UnityEditor;
using System.Collections.Generic;

namespace BrokenFury.Test
{
    [CreateAssetMenu]
    public class Player : ScriptableObject
    {
        [SerializeField] private string _name;
        [SerializeField] private Color _color;
        [SerializeField] private List<Tile> _tiles;
        [SerializeField] private List<Entity> _units;
        [SerializeField] private int _population;
        [SerializeField] private int _ore;
        [SerializeField] private int _fuel;
        [SerializeField] private int _food;

        public string Name => _name;
        public Color Color => _color;
        public List<Tile> Tiles => _tiles;
        public List<Entity> Units => _units;
        public int Population => _population;
        public int Ore => _ore;
        public int Food => _food;
        public int Fuel => _fuel;

        public void ChangeOre(int amount)
        {
            _ore += amount;
        }

        public void ChangePop(int amount)
        {
            _population += amount;
        }

        public void ChangeFood(int amount)
        {
            _food += amount;
        }
        public void ChangeFuel(int amount)
        {
            _fuel += amount;
        }

        public bool ConsumeFood(int f)
        {
            Debug.Log("Checking available food...");

            if (_food > f)
            {
                Debug.Log("...Consuming " + f.ToString() + " food. " + _food.ToString() + " food is left.");
                _food -= f;
                return true;
            }
            Debug.Log("...Not enough food, nothing will be produced.");
            return false;
        }

        public bool ConsumeFuel(int f)
        {
            Debug.Log("Checking available fuel...");

            if (_fuel >= f)
            {
                Debug.Log("...Consuming " + f.ToString() + " fuel. " + _fuel.ToString() + " fuel is left.");
                _fuel -= f;
                return true;
            }
            Debug.Log("...Not enough fuel.");
            return false;
        }
        public void NewGame()
        {
            _tiles.Clear();
            _units.Clear();
            _population = 10;
            _ore = 300;
            _fuel = 100;
            _food = 100;
        }
    }
}