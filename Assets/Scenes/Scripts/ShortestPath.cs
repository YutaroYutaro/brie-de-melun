﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShortestPath
{
    Nodes Nodes;

    public Nodes[,] DijkstraAlgorithm(int startX, int startZ)
    {
        int maxX = 4;
        int maxZ = 6;

        Nodes = new Nodes();

        Nodes[,] nodes = Nodes.CreateNodes(4, 6);

        nodes[startX, startZ].cost = 0;

        while (true)
        {
            Nodes processNode = null;

            for (int posX = 0; posX <= maxX; posX++)
            {
                for (int posZ = 0; posZ <= maxZ; posZ++)
                {
                    Nodes node = nodes[posX, posZ];

                    if (node.done || node.cost < 0)
                    {
                        continue;
                    }

                    if (processNode == null)
                    {
                        processNode = node;
                        continue;
                    }

                    if (node.cost < processNode.cost)
                    {
                        processNode = node;
                    }
                }
            }

            if (processNode == null)
            {
                break;
            }

            processNode.done = true;

            for (int i = 0; i < processNode.edgesTo.Count; i++)
            {
                Nodes node = processNode.edgesTo[i];
                int cost = processNode.cost + processNode.edgesCost[i];

                bool needsUpadate = (node.cost < 0) || (node.cost > cost);

                if (needsUpadate)
                {
                    node.cost = cost;
                    node.previousNodes = processNode;
                }
            }
        }
        return nodes;
    }
}
