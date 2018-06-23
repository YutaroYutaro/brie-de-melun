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

    public Nodes[,] CreateNodes(int maxX = 4, int maxZ = 6)
    {
        Nodes[,] nodes = new Nodes[maxX + 1, maxZ + 1];

        for (int posX = 0; posX <= maxX; posX++)
        {
            for (int posZ = 0; posZ <= maxZ; posZ++)
            {
                nodes[posX, posZ] = new Nodes();
                nodes[posX, posZ].idX = posX;
                nodes[posX, posZ].idZ = posZ;
            }
        }

        for (int posX = 0; posX <= maxX; posX++)
        {
            for (int posZ = 0; posZ <= maxZ; posZ++)
            {
                if (posX == 0 && posZ == 0) {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));

                }
                else if (posX == 0 && posZ == maxZ)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));

                }
                else if (posX == maxX && posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));

                }
                else if (posX == maxX && posZ == maxZ)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));
                }
                else if (posX == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));

                }
                else if (posX == maxX)
                {
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));

                }
                else if (posZ == 0)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));

                }
                else if (posZ == maxZ)
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));

                }
                else
                {
                    nodes[posX, posZ].AddNode(nodes[posX + 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX - 1, posZ], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ + 1], Random.Range(0, 3));
                    nodes[posX, posZ].AddNode(nodes[posX, posZ - 1], Random.Range(0, 3));

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
