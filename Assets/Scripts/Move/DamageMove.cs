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

        public override void Execute(ref Unit attackerUnit, ref Unit defenderUnit)
        {
            if (type == MoveType.Physical)
            {
                defenderUnit.currentHP -= ((power * attackerUnit.stats.force) / defenderUnit.stats.physicalResistence);
            }
            else if (type == MoveType.Magical)
            {
                defenderUnit.currentHP -= ((power * attackerUnit.stats.intelligence) / defenderUnit.stats.magicResistence);
            }

            if (defenderUnit.currentHP < 0)
                defenderUnit.currentHP = 0;
        }
    }
}
