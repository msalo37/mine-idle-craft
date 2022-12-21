using System;
using System.Collections.Generic;
using UnityEngine;

namespace Materials
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Materials/Mineable Material", fileName = "New mining material")]
    public class MineableMaterial : BaseMaterial
    {
        [Tooltip("This sprite will be shown during mining.")]
        public Sprite mineSprite;
        public MineRecipe mineRecipe;

        private void Awake()
        {
            mineRecipe.toolsToMine.Sort((a,b) => a.mineDuration.CompareTo(b.mineDuration));
        }
    }
    
    [Serializable]
    public class MineRecipe
    {
        public List<MiningToolRecipe> toolsToMine;
        public long getAmount = 1;
    }

    [Serializable]
    public class MiningToolRecipe
    {
        [Tooltip("If you dont need tool to mine this resource leave field 'tool' empty")] 
        public BaseMaterial tool;
        public float mineDuration;
    }
}