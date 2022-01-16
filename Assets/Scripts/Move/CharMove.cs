using UnityEngine;

namespace vpet
{
    public abstract class CharMove : ScriptableObject
    {
        public string moveName;
        public int cost;

        public abstract void Execute(ref Unit attackerUnit, ref Unit defenderUnit);
    }
}