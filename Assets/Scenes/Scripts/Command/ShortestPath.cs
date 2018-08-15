using UnityEngine;

public class ShortestPath
{
    //ダイクストラ法
    public Nodes[,] DijkstraAlgorithm(int startX, int startZ, int unitType)
    {
        int maxX = 5;
        int maxZ = 7;

        Nodes nodes = new Nodes();

        Nodes[,] nodesTable = nodes.CreateNodes(5, 7, unitType);

        nodesTable[startX, startZ].Cost = 0;

        while (true)
        {
            Nodes processNode = null;

            for (int posX = 0; posX < maxX; posX++)
            {
                for (int posZ = 0; posZ < maxZ; posZ++)
                {
                    Nodes node = nodesTable[posX, posZ];

                    Debug.Log(posX + ", " + posZ + ": cost = " + node.Cost);

                    if (node.Done || node.Cost < 0)
                    {
                        continue;
                    }

                    if (processNode == null)
                    {
                        processNode = node;
                        continue;
                    }

                    if (node.Cost < processNode.Cost)
                    {
                        processNode = node;
                    }
                }
            }

            if (processNode == null)
            {
                break;
            }

            processNode.Done = true;

            for (int i = 0; i < processNode.EdgesTo.Count; i++)
            {
                Nodes node = processNode.EdgesTo[i];
                int cost = processNode.Cost + processNode.EdgesCost[i];

                bool needsUpadate = (node.Cost < 0) || (node.Cost > cost);

                if (needsUpadate)
                {
                    node.Cost = cost;
                    node.PreviousNodes = processNode;
                }
            }
        }

        return nodesTable;
    }
}