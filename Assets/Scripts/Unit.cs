using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace vpet
{
    public class Unit : MonoBehaviour
    {
        public int level;
        public Sprite sprite;
        public int currentHP;
        public int maxHP;
        public CharStats stats;
        public CharMove[] moves;
    }
}