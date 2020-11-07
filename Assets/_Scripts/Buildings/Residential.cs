using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BrokenFury.Test
{
    public class Residential : Building
    {
        public override void CollectStorage()
        {
            if (Owner.ConsumeFuel(CollectionFuelCost))
            {
                Owner.ChangePop(Storage);
                EmptyStorage();
            }
        }
    }
}
