using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnityEngine;

namespace Assets._PC.Scripts.Core.Data.Config
{
    [CreateAssetMenu(fileName = "NewFakeConfig", menuName = "CreateFakeConfig")]
    public class FakeConfig : ScriptableObject
    {
        [TextArea(0, 200)]
        public string Text;
    }
}
