using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes {

    public List<Nodes> edgesTo = new List<Nodes>(); //移動先のノード
    public List<int> edgesCost = new List<int>();   //移動先の重み
    public bool done = false;                       //探索済みか否か
    public int cost = -1;                           //現在位置の重み-1に設定
    public int idX = 0;                             //ノードのマップ上x座標
    public int idZ = 0;                             //ノードのマップ上z座標
    public Nodes previousNodes = null;              //最短経路の次ノード

    GameObject Map;         //Mapオブジェクトを格納するオブジェクト
    CreateMap CreateMap;    //MapオブジェクトのCreateMapコンポーネントを格納するオブジェクト

    //探索するためのノード群を生成するメソッド
    public Nodes[,] CreateNodes(int maxX = 5, int maxZ = 7)
    {
        //マップをノードの二次元配列で表現
        Nodes[,] nodes = new Nodes[maxX, maxZ];

        //Mapオブジェクトを取得
        Map = GameObject.Find("Map");

        //MapオブジェクトからCreateMapコンポーネントを取得
        CreateMap = Map.GetComponent<CreateMap>();

        //CreateMapコンポーネントからマップの重み表を取得
        int[,] mapWeight = CreateMap.GetMapWeight();

        //個々のノードを生成
        for (int posX = 0; posX < maxX; posX++)
        {
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                nodes[posX, posZ] = new Nodes();
                nodes[posX, posZ].idX = posX;
                nodes[posX, posZ].idZ = posZ;
            }
        }

        //ノードの結合と重みを付加
        for (int posX = 0; posX < maxX; posX++)
        {
            //場所により結合できるノードが違うので場合分け
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                //左下角
                if (posX == 0 && posZ == 0) {
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
    private void AddNode(Nodes node, int cost)
    {
        this.edgesTo.Add(node);
        this.edgesCost.Add(cost);
    }

}
