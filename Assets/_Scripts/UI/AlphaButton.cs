using UnityEngine;
using System.Collections;
using UnityEngine.UI; // Required when Using UI elements.

namespace BrokenFury.Test
{
    public class AlphaButton : MonoBehaviour
    {
        public float AlphaThreshold = 0.1f;

        void Start()
        {
            this.GetComponent<Image>().alphaHitTestMinimumThreshold = AlphaThreshold;
        }
    }
}