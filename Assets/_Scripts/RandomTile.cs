using UnityEngine;
using UnityEngine.Tilemaps;
using System.Collections.Generic;
using System;
using System.Linq;

[Serializable]
public class TileWeight {
    public Sprite sprite;
    public float weight = 1;
}

[CreateAssetMenu(fileName="New Random Tile", menuName="2D/Tiles/Random Tile")]
public class RandomTile : TileBase
{
    
    public List<TileWeight> tiles = new();

    /// <summary>
    /// Retrieves any tile rendering data from the scripted tile.
    /// </summary>
    /// <param name="position">Position of the tile on the Tilemap.</param>
    /// <param name="tilemap">The Tilemap the tile is present on.</param>
    /// <param name="tileData">Data to render the tile.</param>
    public override void GetTileData(Vector3Int position, ITilemap tilemap, ref TileData tileData)
    {

        float sum = (from tile in tiles select tile.weight).Sum();

        float rand = Mathf.Abs(Mathf.Sin(Vector2.Dot(new Vector2(position.x+0.1242f, position.y+6.3269f),new Vector2(12.9898f,78.233f)))*43758.5453123f) % 1 * sum;

        Sprite selected = null;
        foreach (TileWeight tile in tiles) {
            if (rand < tile.weight) {
                selected = tile.sprite;
                break;
            }
            rand -= tile.weight;
        }

        tileData.sprite = selected;
        tileData.flags = TileFlags.LockTransform | TileFlags.LockColor;
        tileData.colliderType = Tile.ColliderType.Sprite;
    }

}
