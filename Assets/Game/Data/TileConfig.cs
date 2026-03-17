using System;
using UnityEngine;

namespace Game.Data
{
    [CreateAssetMenu(fileName = "TileConfig", menuName = "Game/Data")]
    public class TileConfig : ScriptableObject
    {
        [SerializeField]
        private TileData[] _tiles;

        public TileData GetTileData(int value)
        {
            foreach (TileData tile in _tiles)
            {
                if (tile.value == value)
                {
                    return tile;
                }
            }
            return default;
        }
    }

    [Serializable]
    public struct TileData
    {
        public int value;
        public string name;
        public Color color;
        public Sprite icon;
    }
}
