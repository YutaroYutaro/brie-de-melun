using System.Collections.Generic;
using System;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    float distance = 0.0f;
    //Nodes nodes;
    Nodes[,] resultNodes;
    ShortestPath ShortestPath;

    Vector3 worldPoint;
    Vector3 _nextDestination;

    //[SerializeField]
    //RectTransform rectTran;
    
    private List<GameObject> _unitList;

    void Start()
    {
        //メインカメラとオブジェクトの距離を測定
        distance = Vector3.Distance(transform.position, Camera.main.transform.position);

        _unitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetUnitList();
        //Debug.Log("distance ->" + distance);

        //nodes = new Nodes();

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

            Ray ray = Camera.main.ScreenPointToRay(mousePos);

            RaycastHit hit;

            int endPointX;
            int endPointZ;

            if (Physics.Raycast(ray, out hit, Mathf.Infinity))
            {
                //Debug.DrawRay(ray.origin, mousePos);
                endPointX = Mathf.RoundToInt(hit.point.x);
                endPointZ = Mathf.RoundToInt(hit.point.z);
            }
            else
            {
                endPointX = Mathf.RoundToInt(transform.position.x);
                endPointZ = Mathf.RoundToInt(transform.position.z);
            }

            //ダイクストラ法で最短経路を検索
            resultNodes = ShortestPath.DijkstraAlgorithm(endPointX, endPointZ);

            //現在地を目的地として設定
            //ノードが後ろから繋がっているため
            Nodes unitPositionNode = resultNodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)];
            
            for (int i = 0; i < _unitList.Count; i++)
            {
                //Debug.Log("List[" + i + "] : (" + Mathf.RoundToInt(_unitList[i].transform.position.x) + ", " + Mathf.RoundToInt(_unitList[i].transform.position.z) + ")");
            }

            Debug.Log("========================================");

            string path = "Start -> ";
            Nodes currentNode = unitPositionNode;

            while (true)
            {
                //ひとつ前のノードを参照
                Nodes nextNode = currentNode.previousNodes;

                //クリックしたノードに達するとループを抜ける
                if (nextNode == null || this.ExistUnit(nextNode.idX, nextNode.idZ))
                {
                    path += " Goal";
                    break;
                }
                
                //次に移動するノードの座標
                _nextDestination.x = nextNode.idX;
                _nextDestination.y = 1;
                _nextDestination.z = nextNode.idZ;

                //次に移動するノードに移動
                transform.DOMove(_nextDestination, 0.4f);

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

    private bool ExistUnit(int x, int z)
    {
        for (int i = 0; i < _unitList.Count; i++)
        {
            if (Mathf.RoundToInt(_unitList[i].transform.position.x) == x &&
                Mathf.RoundToInt(_unitList[i].transform.position.z) == z)
            {
                return true;
            }
        }
        return false;
    }

    public async void MiniMapClickUnitMove(int clickMiniMapImageInstancePositionX, int clickMiniMapImageInstancePositionZ)
    {
        //ダイクストラ法で最短経路を検索
        resultNodes = ShortestPath.DijkstraAlgorithm(clickMiniMapImageInstancePositionX, clickMiniMapImageInstancePositionZ);

        //現在地を目的地として設定
        //ノードが後ろから繋がっているため
        Nodes unitPositionNode = resultNodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)];            

        Debug.Log("========================================");

        string path = "Start -> ";
        Nodes currentNode = unitPositionNode;

        while (true)
        {
            //ひとつ前のノードを参照
            Nodes nextNode = currentNode.previousNodes;

            //クリックしたノードに達するとループを抜ける
            if (nextNode == null || this.ExistUnit(nextNode.idX, nextNode.idZ))
            {
                path += " Goal";
                break;
            }
                
            //次に移動するノードの座標
            _nextDestination.x = nextNode.idX;
            _nextDestination.y = 1;
            _nextDestination.z = nextNode.idZ;

            //次に移動するノードに移動
            transform.DOMove(_nextDestination, 0.4f);

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