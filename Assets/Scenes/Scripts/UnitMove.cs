using System.Collections;
using System;
using System.Threading;
using UniRx;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    float distance = 0.0f;
    Nodes nodes;
    Nodes[,] resultNodes;
    ShortestPath ShortestPath;

    Vector3 worldPoint;
    Vector3 destination;

    [SerializeField]
    RectTransform rectTran;

    void Start()
    {
        //メインカメラとオブジェクトの距離を測定
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);
        Debug.Log("distance ->" + distance);

        nodes = new Nodes();

        ShortestPath = new ShortestPath();

    }

    async void Update()
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

            //ダイクストラ法で最短経路を検索
            resultNodes = ShortestPath.DijkstraAlgorithm(endPointX, endPointZ);

            //現在地を目的地として設定
            //ノードが後ろから繋がっているため
            Nodes goalNode = resultNodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)];

            Debug.Log("========================================");

            string path = "Start -> ";
            Nodes currentNode = goalNode;

            while (true)
            {
                //ひとつ前のノードを参照
                Nodes nextNode = currentNode.previousNodes;

                //クリックしたノードに達するとループを抜ける
                if (nextNode == null)
                {
                    path += " Goal";
                    break;
                }
                
                //次に移動するノードの座標
                destination.x = nextNode.idX;
                destination.y = 1;
                destination.z = nextNode.idZ;

                //次に移動するノードに移動
                transform.DOMove(destination, 0.4f);

                //待機
                await Task.Delay(TimeSpan.FromSeconds(0.5f));

                path += nextNode.idX.ToString() + nextNode.idZ.ToString() +  " -> ";

                //次のノードへ
                currentNode = nextNode;
            }

            Debug.Log(path);
            Debug.Log("========================================");

            
        }
    }
}