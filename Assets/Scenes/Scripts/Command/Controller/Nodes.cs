using System.Collections.Generic;
using UnityEngine;

public class Nodes
{
    public readonly List<Nodes> EdgesTo = new List<Nodes>(); //移動先のノード
    public readonly List<int> EdgesCost = new List<int>(); //移動先の重み
    public bool Done = false; //探索済みか否か
    public int Cost = -1; //現在位置の重み-1に設定
    public int IdX = 0; //ノードのマップ上x座標
    public int IdZ = 0; //ノードのマップ上z座標
    public Nodes PreviousNodes = null; //最短経路の次ノード

    //探索するためのノード群を生成するメソッド
    public Nodes[,] CreateNodes(int maxX = 5, int maxZ = 7)
    {
        //マップをノードの二次元配列で表現
        Nodes[,] nodes = new Nodes[maxX, maxZ];

        //CreateMapコンポーネントからマップの重み表を取得
        int[,] mapWeight = GameObject.Find("Map").GetComponent<CreateMap>().GetMapWeight();

        //個々のノードを生成
        for (int posX = 0; posX < maxX; posX++)
        {
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                nodes[posX, posZ] = new Nodes()
                {
                    IdX = posX,
                    IdZ = posZ
                };
            }
        }

        //ノードの結合と重みを付加
        for (int posX = 0; posX < maxX; posX++)
        {
            //場所により結合できるノードが違うので場合分け
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                //左下角
                if (posX == 0 && posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                }
                //左上角
                else if (posX == 0 && posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                //右下角
                else if (posX == maxX - 1 && posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                }
                //右上角
                else if (posX == maxX - 1 && posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                //左辺
                else if (posX == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                //右辺
                else if (posX == maxX - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                //下辺
                else if (posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                }
                //上辺
                else if (posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                //それ以外
                else
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
            }
        }

        return nodes;
    }

    //ノードを結合し重みを付加するメソッド
    private void AddNode(Nodes node, int mapObjectWeight)
    {
        EdgesTo.Add(node);
        EdgesCost.Add(mapObjectWeight);
    }
}