using System;
using System.Collections.Generic;
using UnityEngine;

namespace Materials
{
    [CreateAssetMenu(menuName = "Scriptable Objects/Materials/Mineable Material", fileName = "New mining material")]
    public class MineableMaterial : BaseMaterial
    {
        public MineRecipe mineRecipe;
    }
    
    [Serializable]
    public class MineRecipe
    {
        public List<MiningToolRecipe> toolsToMine;
    }

    [Serializable]
    public class MiningToolRecipe
    {
        [Tooltip("If you dont need tool to mine this resource leave field 'tool' empty")] 
        public BaseMaterial tool;
        public float mineDuration;
    }
}