using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Internal;
using UnityEngine.Tilemaps;

namespace LDtkUnity
{
    /// <summary>
    /// Stores the autogenerated sprites for the tiles, sprites for level backgrounds, and all TileBases for the Int Grid values, and for the LDtk tiles.
    /// </summary>
    [HelpURL(LDtkHelpURL.SO_ARTIFACT_ASSETS)]
    public class LDtkArtifactAssets : ScriptableObject
    {
        [ExcludeFromDocs] public const string PROP_SPRITE_LIST = nameof(_cachedSprites);
        [ExcludeFromDocs] public const string PROP_TILE_LIST = nameof(_cachedTiles);
        [ExcludeFromDocs] public const string PROP_BACKGROUND_LIST = nameof(_cachedBackgrounds);

        [SerializeField] private List<Sprite> _cachedSprites = new List<Sprite>();
        [SerializeField] private List<TileBase> _cachedTiles = new List<TileBase>();
        [SerializeField] private List<Sprite> _cachedBackgrounds = new List<Sprite>();
        
        /// <summary>
        /// Get a sprite by name from this import result.
        /// </summary>
        /// <param name="spriteName">
        /// The name of the sprite asset.
        /// </param>
        /// <returns>
        /// The sprite that was generated in this import result.
        /// </returns>
        public Sprite GetSpriteByName(string spriteName) => GetItem(spriteName, _cachedSprites);
        
        /// <summary>
        /// Get a tile by name from this import result.
        /// </summary>
        /// <param name="tileName">
        /// The name of the tile asset.
        /// </param>
        /// <returns>
        /// The tile that was generated in this import result.
        /// </returns>
        public TileBase GetTileByName(string tileName) => GetItem(tileName, _cachedTiles);
        private T GetItem<T>(string assetName, IReadOnlyCollection<T> array) where T : Object
        {
            if (string.IsNullOrEmpty(assetName))
            {
                Debug.LogError("Tried getting an asset without a name");
                return null;
            }
            
            if (array.IsNullOrEmpty())
            {
                return null;
            }
            
            return array.FirstOrDefault(p => p.name.Equals(assetName));
        }

        [ExcludeFromDocs]
        public bool AddArtifact(Object obj)
        {
            switch (obj)
            {
                case Sprite sprite:
                    _cachedSprites.Add(sprite);
                    return true;
                
                case TileBase tile:
                    _cachedTiles.Add(tile);
                    return true;
                
                default:
                    return false;
            }
        }

        [ExcludeFromDocs]
        public void AddBackground(Sprite obj)
        {
            _cachedBackgrounds.Add(obj);
        }
        
        [ExcludeFromDocs]
        public void HideSprites()
        {
            HideGroup(_cachedSprites);
        }
        
        [ExcludeFromDocs]
        public void HideTiles()
        {
            HideGroup(_cachedTiles);
        }

        //hide the backgrounds so they arent packed in an atlas
        [ExcludeFromDocs]
        public void HideBackgrounds()
        {
            HideGroup(_cachedBackgrounds);
        }

        private void HideGroup<T>(List<T> list) where T : Object
        {
            foreach (T obj in list)
            {
                if (obj == null)
                {
                    Debug.Log("null object");
                    return;
                }
                obj.hideFlags = HideFlags.HideInHierarchy;
            }
        }

        [ExcludeFromDocs]
        public bool ContainsBackground(Sprite sprite)
        {
            return sprite != null && !_cachedBackgrounds.IsNullOrEmpty() && _cachedBackgrounds.Contains(sprite);
        }
    }
}