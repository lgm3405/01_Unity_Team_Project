using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveLoad
{
    public int gameTime = default;
    public int[] savePoint = new int[2];
    public bool[] eventCheck;
    public int[] positionX;
    public int[] positionY;
    public int money = default;

    public SaveLoad(int gameTime_, int[] savePoint_, bool[] eventCheck_, int[] positionX_, int[] positionY_, int money_)
    {
        gameTime = gameTime_;
        savePoint = savePoint_;
        eventCheck = eventCheck_;
        positionX = positionX_;
        positionY = positionY_;
        money = money_;
    }
}