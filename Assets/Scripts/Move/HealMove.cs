using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vpet
{
    [CreateAssetMenu(fileName = "HealMove", menuName = "V-pet/Move/Heal Move", order = 1)]
    public class HealMove : CharMove
    {
        public int power;

        public override void Execute(ref Unit attackerUnit, ref Unit defenderUnit)
        {
            attackerUnit.currentHP += power * attackerUnit.stats.intelligence;

            if (attackerUnit.currentHP > attackerUnit.MaxHP)
                attackerUnit.currentHP = attackerUnit.MaxHP;
        }
    }
}