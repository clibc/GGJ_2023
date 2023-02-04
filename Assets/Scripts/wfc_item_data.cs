using UnityEngine;

[CreateAssetMenu(fileName = "WFCItem", menuName = "ScriptableObjects/WFCItem")]
public class wfc_item_data : ScriptableObject
{
    public byte ItemID;
    public Sprite Sprite;
    public byte[] PossibleItemsTop;
    public byte[] PossibleItemsBottom;
    public byte[] PossibleItemsLeft;
    public byte[] PossibleItemsRight;

    public bool ValidateItem(byte CompareItemID, tile_direction Direction)
    {
        switch (Direction)
        {
            case tile_direction.Top:
                return _Contains(PossibleItemsTop, CompareItemID);
            case tile_direction.Bottom:
                return _Contains(PossibleItemsBottom, CompareItemID);
            case tile_direction.Left:
                return _Contains(PossibleItemsLeft, CompareItemID);
            case tile_direction.Right:
                return _Contains(PossibleItemsRight, CompareItemID);
            default:
                return false;
        }
    }

    bool _Contains(byte[] PossibleItems, byte CompareItemID)
    {
        foreach(var I in PossibleItems)
        {
            if(CompareItemID == I)
                return true;
        }
        return false;
    }
}