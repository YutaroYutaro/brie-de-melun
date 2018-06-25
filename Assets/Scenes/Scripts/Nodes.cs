using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Nodes {

    public List<Nodes> edgesTo = new List<Nodes>();
    public List<int> edgesCost = new List<int>();
    public bool done = false;
    public int cost = -1;
    public int idX = 0;
    public int idZ = 0;
    public Nodes previousNodes = null;

    GameObject Map;

    CreateMap CreateMap;

    public Nodes[,] CreateNodes(int maxX = 5, int maxZ = 7)
    {
        Nodes[,] nodes = new Nodes[maxX, maxZ];

        Map = GameObject.Find("Map");

        CreateMap = Map.GetComponent<CreateMap>();

        int[,] mapWeight = CreateMap.GetMapWeight();

        for (int posX = 0; posX < maxX; posX++)
        {
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                nodes[posX, posZ] = new Nodes();
                nodes[posX, posZ].idX = posX;
                nodes[posX, posZ].idZ = posZ;
            }
        }

        for (int posX = 0; posX < maxX; posX++)
        {
            for (int posZ = 0; posZ < maxZ; posZ++)
            {
                if (posX == 0 && posZ == 0) {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);

                }
                else if (posX == 0 && posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);

                }
                else if (posX == maxX - 1 && posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);

                }
                else if (posX == maxX - 1 && posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);
                }
                else if (posX == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);

                }
                else if (posX == maxX - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);

                }
                else if (posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], mapWeight[posX, posZ]);

                }
                else if (posZ == maxZ - 1)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], mapWeight[posX, posZ]);
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], mapWeight[posX, posZ]);

                }
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

    private void AddNode(Nodes node, int cost)
    {
        this.edgesTo.Add(node);
        this.edgesCost.Add(cost);
    }

}
