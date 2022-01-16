using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vpet
{
    [CreateAssetMenu(fileName = "DamageMove", menuName = "V-pet/Move/Damage Move", order = 0)]
    public class DamageMove : CharMove
    {
        public MoveType type;
        public MoveElement element;
        public int power;
        [Range(0, 1)] public float accuracy;
    }
}
