using System;
using System.Collections.Generic;

namespace Assets._PC.Scripts.Core.Data.Currency
{
    [Serializable]
    public class CurrencyData
    {
        public Dictionary<CurrencyType, int> Data = new();
    }
}
