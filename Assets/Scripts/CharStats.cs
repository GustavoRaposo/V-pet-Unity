using UnityEngine;

namespace vpet
{
    [CreateAssetMenu(fileName = "Stats", menuName = "V-pet/CharStats", order = 0)]
    public class CharStats : ScriptableObject
    {
        public string unitName;
        public int hp;
        public int mp;
        public int force;
        public int intelligence;
        public int magicResistence;
        public int physicalResistence;
        public int speed;
    }
}