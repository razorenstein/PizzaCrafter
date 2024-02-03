using Assets._PC.Scripts.Core.Data.Board;
using Assets._PC.Scripts.Core.Data.Ingredients.Abstract;

namespace Assets._PC.Scripts.Core.Data
{
    public interface IMerge
    {
        bool TryMerge(TileData first, TileData second, out TileData merged);
    }
}
