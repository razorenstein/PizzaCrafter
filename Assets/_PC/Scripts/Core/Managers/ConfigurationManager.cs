using Assets._PC.Scripts.Core.Data.Config;
using Newtonsoft.Json;
using System;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Managers
{
    public class ConfigurationManager
    {
        public void GetConfig<T>(Action<T> onComplete) where T : BaseConfig
        {
            var config = Resources.Load<FakeConfig>(typeof(T).Name);
            var returnValue = JsonConvert.DeserializeObject<T>(config.Text);
            onComplete.Invoke(returnValue);
        }
    }
}

