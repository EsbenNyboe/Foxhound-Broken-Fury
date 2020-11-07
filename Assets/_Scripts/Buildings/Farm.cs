using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Farm : Building
    {
        public override void CollectStorage()
        {
            if (Owner.ConsumeFuel(CollectionFuelCost))
            {
                Owner.ChangeFood(Storage);
                EmptyStorage();
            }
        }
    }
}