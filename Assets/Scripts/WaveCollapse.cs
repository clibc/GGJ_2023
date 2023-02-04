using UnityEngine;
using System.Collections.Generic;

public struct tile
{
    public List<byte> PossibleItems;
    public int Entropy => PossibleItems.Count;
    public bool IsValid => PossibleItems != null;

    public tile(List<byte> PossibleItems)
    {
        this.PossibleItems = PossibleItems;
    }
}

public enum tile_direction
{
    Top    = 0,
    Bottom = 1,
    Left   = 2,
    Right  = 3,
}

public class WaveCollapse : MonoBehaviour
{
    [SerializeField] SpriteRenderer SpritePrefab;
    [SerializeField] Sprite[] Sprites;

    const int GridWidth = 3;
    const int GridHeight = 3;
    tile[,] Tiles = new tile[GridWidth, GridHeight];

    public wfc_item_data[] WFCItems;

    void Start()
    {
        for (int Y = 0; Y < GridHeight; ++Y)
        {
            for (int X = 0; X < GridWidth; ++X)
            {
//              var S = Instantiate(SpritePrefab, new Vector3(X*0.5f, Y*0.5f, 0), Quaternion.identity, transform);
//              S.sprite = Sprites[Random.Range(0, Sprites.Length - 1)];

                Tiles[X,Y] = new tile(new List<byte>(WFCItems.Length));
                for(byte I = 0; I < WFCItems.Length; ++I)
                {
                    Tiles[X,Y].PossibleItems.Add(I);
                }
            }
        }

        CollapseTile(0, 0);
    }

    public void CollapseTile(int X, int Y)
    {
        tile T = Tiles[X, Y];

        // Choose random one
        int RandomIndex = Random.Range(0, T.Entropy - 1);
        byte SelectedTile = T.PossibleItems[RandomIndex];
        T.PossibleItems.Clear();
        T.PossibleItems.Add(SelectedTile);

        var S = Instantiate(SpritePrefab, new Vector3(X*0.5f, Y*-0.5f, 0), Quaternion.identity, transform);
        S.sprite = WFCItems[SelectedTile].Sprite;
        Propogate(Tiles[X+1, Y], X+1, Y, tile_direction.Right);
    }

    public void Propogate(tile Tile, int X, int Y,  tile_direction PrevTile)
    {
        // propogate top, bottom, left, right but exept PrevTile
        if(Tile.Entropy == 1)
            return;

        tile[] NeighbourTiles = { GetTile(X, Y - 1), // top
                                  GetTile(X, Y + 1), // bot
                                  GetTile(X-1, Y),   // left
                                  GetTile(X+1, Y)};  // right

        // validate tiles
        bool AnyItemRemoved = false;
        foreach (byte I in Tile.PossibleItems)
        {
            wfc_item_data WFCItem = WFCItems[I];
            bool IsAllPossible = true;

            for(int NeighbourIndex = 0; NeighbourIndex < 4; ++NeighbourIndex)
            {
                if(!IsAllPossible)
                    continue;
                
                tile Neighbour = NeighbourTiles[NeighbourIndex];
                if (!Neighbour.IsValid) // Outside of the grid
                    continue;
                foreach (byte NeighbourItem in Neighbour.PossibleItems)
                {
                    IsAllPossible &= WFCItem.ValidateItem(NeighbourItem, (tile_direction)NeighbourIndex);
                }
            }

            if(IsAllPossible)
            {
                Tile.PossibleItems.Remove(I);
                AnyItemRemoved = true;
            }
        }

        if(AnyItemRemoved)
        {
            //Propogate()
        }
    }

    tile GetTile(int X, int Y)
    {
        if(X < 0 || X >= GridWidth ||
           Y < 0 || Y >= GridHeight)
        {
            return new tile(null);
        }
        return Tiles[X, Y];
    }
}