using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vpet
{
    [CreateAssetMenu(fileName = "SupportMove", menuName = "V-pet/Move/Support Move", order = 2)]
    public class SupportMove : CharMove 
    {
        public override void Execute(ref Unit attackerUnit, ref Unit defenderUnit)
        {
        }
    }
}
