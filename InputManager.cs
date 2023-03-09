using System;
using System.Collections.Generic;
using System.Text;
using System.Numerics;

public class InputManager
{
    public enum Keys
    {
        None,
        MouseX,
        MouseY,
        Space,
        LeftShift,
        GetAxisRaw,
        Escape,
        W,
        A,
        S,
        D,
        Z,
        F,
        G,
        R,
        C,
        Q,
        E,
        Mouse1,
        Mouse2,
        MouseScroll,
    }

    public static void TryToMove(int connectionID, Keys key)
    {
        Vector3 tmpPosition = GameManager.playerList[connectionID].position;

        if (key == Keys.None) return;

        Player player = GameManager.playerList[connectionID];

        if(key == Keys.W)
        {
            //Logic for movement
            tmpPosition.X += GameManager.playerSpeed * ConvertRotationSin(player.rotation); 
            tmpPosition.Z += GameManager.playerSpeed * ConvertRotationCos(player.rotation);
        }
        else if (key == Keys.S)
        {
            //Logic for movement
            tmpPosition.X -= GameManager.playerSpeed * ConvertRotationSin(player.rotation);
            tmpPosition.Z -= GameManager.playerSpeed * ConvertRotationCos(player.rotation);
        }
        else if (key == Keys.A)
        {
            //Logic for movement
            tmpPosition.X -= GameManager.playerSpeed * ConvertRotationCos(player.rotation);
            tmpPosition.Z += GameManager.playerSpeed * ConvertRotationSin(player.rotation);
        }
        else if (key == Keys.D)
        {
            //Logic for movement
            tmpPosition.X += GameManager.playerSpeed * ConvertRotationCos(player.rotation);
            tmpPosition.Z -= GameManager.playerSpeed * ConvertRotationSin(player.rotation);
        }
        GameManager.playerList[connectionID].position = tmpPosition;

        

        NetworkSend.SendPlayerMove(connectionID, GameManager.playerList[connectionID].position.X, GameManager.playerList[connectionID].position.Y, GameManager.playerList[connectionID].position.Z);
    }

    //Math wizadry, courtesy of google. don't ask, I can't tell you.
    public static float ConvertRotationSin(float rotation)
    {
        return (float)Math.Round(Math.Sin(rotation * (Math.PI / 180)), 4);
    }

    //Math wizadry, courtesy of google. don't ask, I can't tell you.
    public static float ConvertRotationCos(float rotation)
    {
        return (float)Math.Round(Math.Cos(rotation * (Math.PI / 180)), 4);
    }
}

