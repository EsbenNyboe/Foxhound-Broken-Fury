using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Refinery : Building
    {
        public override void CollectStorage()
        {
            if (Owner.ConsumeFuel(CollectionFuelCost))
            {
                Owner.ChangeFuel(Storage);
                EmptyStorage();
            }
        }
    }
}