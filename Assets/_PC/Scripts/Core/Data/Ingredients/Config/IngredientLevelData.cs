using System;

namespace Assets._PC.Scripts.Core.Data.Ingredients.Config
{
    public class IngredientLevelData 
    {
        public IngredientType Type { get; set; }
        public int Level { get; set; }
        public int MaxLevel { get; set; }
        public string SpriteAddressableKey { get; set; }

        public override bool Equals(object obj)
        {
            if (obj is IngredientLevelData other)           
                return Type == other.Type && Level == other.Level;
            
            return false;
        }

        public override int GetHashCode()
        {
            int hash = 17;
            hash = hash * 23 + Type.GetHashCode();
            hash = hash * 23 + Level.GetHashCode();
            return hash;
        }
    }
}