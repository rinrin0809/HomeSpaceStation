using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Node
{
    public bool Iswall;
    public Vector2 Position;
    public int gridX;
    public int gridY;
    public int Gcost;
    public int Hcost;
    public Node Parent;
    public int Fcost { get { return Gcost + Hcost; } }
    //public int Fcost => Gcost + Hcost;
    public Node(bool a_Iswall,Vector3 a_Pos,int a_gridX,int a_gridY)
    {

        Iswall = a_Iswall;
        Position = a_Pos;
        gridX = a_gridX;
        gridY = a_gridY;
    }


}
