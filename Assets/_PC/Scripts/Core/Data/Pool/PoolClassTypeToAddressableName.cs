using Assets._PC.Scripts.Core.Data.Enums;
using Codice.Client.BaseCommands;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Core.Data.Pool
{
    public class PoolClassTypeToAddressableName
    {
        public static string Map(PoolType type)
        {
            return type switch
            {
                PoolType.Tile => "Tile",
                _ => throw new NotImplementedException("No mapping between pool class type and adressable name")
            };  
        }
    }
}
