using UnityEngine;
using System.Collections;

namespace BrokenFury.Test
{
    public class StateMachine : MonoBehaviour
    {
        protected State State;

        public void SetState(State state)
        {
            State = state;
            StartCoroutine(State.Start());
        }

    }
}