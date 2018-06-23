using System.Collections;
using System;
using System.Threading;
using UniRx;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    float distance = 0.0f;
    Nodes nodes;
    Nodes[,] resultNodes;
    ShortestPath ShortestPath;

    Vector3 worldPoint;

    void Start()
    {
        //メインカメラとオブジェクトの距離を測定
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Debug.Log("distance ->" + distance);

        nodes = new Nodes();

        ShortestPath = new ShortestPath();

    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            //クリックしたスクリーン座標を取得
            Vector3 mousePos = Input.mousePosition;

            //メインカメラとオブジェクトの距離をクリックした座標の高さに代入
            mousePos.z = distance;

            //ワールド座標に変換
            worldPoint = Camera.main.ScreenToWorldPoint(mousePos);

            //座標の四捨五入
            int endPointX = Mathf.RoundToInt(worldPoint.x);
            int endPointZ = Mathf.RoundToInt(worldPoint.z);

            resultNodes = ShortestPath.DijkstraAlgorithm(endPointX, endPointZ);

            Nodes goalNode = resultNodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)];

            Debug.Log("========================================");

            string path = "Start -> ";
            Nodes currentNode = goalNode;

            while (true)
            {
                Nodes nextNode = currentNode.previousNodes;
                if (nextNode == null)
                {
                    path += " Goal";
                    break;
                }

                MoveUnit(nextNode.idX, nextNode.idZ);

                path += nextNode.idX.ToString() + nextNode.idZ.ToString() +  " -> ";

                currentNode = nextNode;
            }

            Debug.Log(path);
            Debug.Log("========================================");
        }
    }

    private void MoveUnit(int x, int z)
    {
        worldPoint.x = x;
        worldPoint.z = z;

        transform.position = worldPoint;
    }
}