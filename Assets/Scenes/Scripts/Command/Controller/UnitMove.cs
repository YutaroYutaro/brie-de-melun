using System.Collections.Generic;
using System;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    Nodes[,] resultNodes;
    ShortestPath ShortestPath;

    Vector3 worldPoint;
    Vector3 _nextDestination;

    private List<GameObject> _unitList;
    private List<GameObject> _enemyUnitList;

    void Start()
    {
        //メインカメラとオブジェクトの距離を測定

        _unitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetMyUnitList();
        _enemyUnitList = GameObject.Find("UnitManager").GetComponent<UnitManager>().GetEnemyUnitList();


        ShortestPath = new ShortestPath();
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

        foreach (GameObject enemyUnit in _enemyUnitList)
        {
            if (Mathf.RoundToInt(enemyUnit.transform.position.x) == x &&
                Mathf.RoundToInt(enemyUnit.transform.position.z) == z)
            {
                GameObject surpriseAttacker = GameObject.Find("FogManager").GetComponent<FogManager>()
                    .SetActiveUnitInFog(x, z);
                if (surpriseAttacker != null)
                {
                    GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>()
                        .SetSurpriseAttacker(surpriseAttacker);
                    GameObject.Find("UnitAttackManager").GetComponent<UnitAttackManager>()
                        .SurpriseAttack(this.gameObject);
                }

                return true;
            }
        }

        return false;
    }

    public async void MiniMapClickUnitMove(int clickMiniMapImageInstancePositionX,
        int clickMiniMapImageInstancePositionZ)
    {
        //ダイクストラ法で最短経路を検索
        resultNodes =
            ShortestPath.DijkstraAlgorithm(clickMiniMapImageInstancePositionX, clickMiniMapImageInstancePositionZ);

        //現在地を目的地として設定
        //ノードが後ろから繋がっているため
        Nodes unitPositionNode =
            resultNodes[Mathf.RoundToInt(transform.position.x), Mathf.RoundToInt(transform.position.z)];

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
            switch (tag)
            {
                case "ProximityAttackUnit":
                case "RemoteAttackUnit":
                    _nextDestination.y = 1;
                    break;
                case "ReconnaissanceUnit":
                    _nextDestination.y = 1.5f;
                    break;
                default:
                    _nextDestination.y = 1;
                    break;
            }

            _nextDestination.z = nextNode.idZ;

            GameObject.Find("FogManager").GetComponent<FogManager>().ClearFog(nextNode.idX, nextNode.idZ);
            GetComponent<UnitOwnIntPosition>().SetUnitOwnIntPosition(nextNode.idX, nextNode.idZ);

            //次に移動するノードに移動
            transform.DOMove(_nextDestination, 0.4f);

            //待機
            await Task.Delay(TimeSpan.FromSeconds(0.5f));

            path += nextNode.idX.ToString() + nextNode.idZ.ToString() + " -> ";

            //次のノードへ
            currentNode = nextNode;
        }

        Debug.Log(path);
        Debug.Log("========================================");
    }
}