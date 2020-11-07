using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Factory : Building
    {
        public override void CollectStorage()
        {
            if (Owner.ConsumeFuel(CollectionFuelCost))
            {
                Owner.ChangeOre(Storage);
                EmptyStorage();
            }
        }
    }
}
