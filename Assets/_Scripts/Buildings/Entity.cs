using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Entity : MonoBehaviour
    {
        private int health;
        private int armour;

        public int Health => health;
        public int Armour => armour;


        public void TakeDamage(int damage)
        {
            health -= damage;

        }

        public bool Destroyed()
        {
            if (Health <= 0)
                return true;
            return false;
        }
    }
}