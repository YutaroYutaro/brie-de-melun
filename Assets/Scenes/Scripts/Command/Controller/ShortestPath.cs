public class ShortestPath
{
    Nodes Nodes;

    //ダイクストラ法
    public Nodes[,] DijkstraAlgorithm(int startX, int startZ)
    {
        int maxX = 5;
        int maxZ = 7;

        Nodes = new Nodes();

        Nodes[,] nodes = Nodes.CreateNodes(5, 7);

        nodes[startX, startZ].Cost = 0;

        while (true)
        {
            Nodes processNode = null;

            for (int posX = 0; posX < maxX; posX++)
            {
                for (int posZ = 0; posZ < maxZ; posZ++)
                {
                    Nodes node = nodes[posX, posZ];

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

        return nodes;
    }
}