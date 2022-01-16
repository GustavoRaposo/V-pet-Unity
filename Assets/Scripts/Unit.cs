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

        public CharStats stats;
        public CharMove[] moves;

        public int MaxHP
        {
            get => stats.hp;
        }

        void Awake()
        {
            currentHP = MaxHP;
        }
    }
}