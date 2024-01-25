using Assets._PC.Scripts.Core.Data.Board;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets._PC.Scripts.Gameplay.Componenets
{
    public class OvenTileView : TileView
    {
        public async override Task OnDragDrop(CellData cellData)
        {
            await BoardView.Instance.OnTileDragDrop(cellData);
        }
    }
}
