using System;
using DG.Tweening;
using System.Threading.Tasks;
using JetBrains.Annotations;
using UnityEngine;
using UniRx.Async;

public class UnitMove : MonoBehaviour
{
    public async UniTask<bool> MiniMapClickUnitMove(
        int clickMiniMapImageInstancePositionX,
        int clickMiniMapImageInstancePositionZ
    )
    {
        ShortestPath shortestPath = new ShortestPath();

        int unitType;

        switch (tag)
        {
            case "ProximityAttackUnit":
                unitType = SummonUnitTypeDefine.SummonUnitType.PROXIMITY;

                break;

            case "RemoteAttackUnit":
                unitType = SummonUnitTypeDefine.SummonUnitType.REMOTE;

                break;

            case "ReconnaissanceUnit":
                unitType = SummonUnitTypeDefine.SummonUnitType.RECONNAISSANCE;

                break;

            default:
                Debug.LogError("Don't Exist unitType.");

                return false;
        }

        //ダイクストラ法で最短経路を検索
        Nodes[,] resultNodes =
            shortestPath.DijkstraAlgorithm(
                clickMiniMapImageInstancePositionX,
                clickMiniMapImageInstancePositionZ,
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ,
                unitType
            );

        //現在地を目的地として設定
        //ノードが後ろから繋がっているため
        Nodes unitPositionNode =
            resultNodes[
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ
            ];

        Debug.Log("MoveCost: " + unitPositionNode.Cost);

        if (unitPositionNode.Cost > GetComponent<UnitStatus>().MovementPoint)
        {
            Debug.Log("Can't move.");
            return false;
        }

        if (GameObject.Find("Player1Units").transform.childCount != 0)
        {
            foreach (Transform child in GameObject.Find("Player1Units").transform)
            {
                if (child.GetComponent<UnitOwnIntPosition>().PosX == clickMiniMapImageInstancePositionX &&
                    child.GetComponent<UnitOwnIntPosition>().PosZ == clickMiniMapImageInstancePositionZ)
                {
                    Debug.Log("There is a My Unit!");
                    return false;
                }
            }
        }

        if (GameObject.Find("Player2Units").transform.childCount != 0)
        {
            foreach (Transform child in GameObject.Find("Player2Units").transform)
            {
                if (child.GetComponent<UnitOwnIntPosition>().PosX == clickMiniMapImageInstancePositionX &&
                    child.GetComponent<UnitOwnIntPosition>().PosZ == clickMiniMapImageInstancePositionZ &&
                    child.gameObject.activeSelf)
                {
                    Debug.Log("There is a Enemy's Unit!");
                    return false;
                }
            }
        }

        Debug.Log("========================================");

        string path = "Start -> ";
        Nodes currentNode = unitPositionNode;

        while (true)
        {
            //ひとつ前のノードを参照
            Nodes nextNode = currentNode.PreviousNodes;

            //クリックしたノードに達するとループを抜ける
            if (nextNode == null || ExistUnit(nextNode.IdX, nextNode.IdZ))
            {
                path += " Goal";
                break;
            }

            GameObject.Find("FogManager")
                .GetComponent<FogManager>()
                .ClearFog(
                    nextNode.IdX,
                    nextNode.IdZ
                );

            int[,] mapWeight = GameObject.Find("Map").GetComponent<CreateMap>().GetMapWeight();

            if (mapWeight[nextNode.IdX, nextNode.IdZ] > GetComponent<UnitStatus>().MovementPoint)
            {
                Debug.Log("Crash!");
                return true;
            }

            float unitTypePosY;

            switch (tag)
            {
                case "ProximityAttackUnit":
                case "RemoteAttackUnit":
                    unitTypePosY = 1;
                    break;
                case "ReconnaissanceUnit":
                    unitTypePosY = 1.5f;
                    break;
                default:
                    unitTypePosY = 1;
                    break;
            }

            Vector3 nextDestination = new Vector3(nextNode.IdX, unitTypePosY, nextNode.IdZ);

            GetComponent<UnitOwnIntPosition>()
                .SetUnitOwnIntPosition(
                    nextNode.IdX,
                    nextNode.IdZ
                );

            //次に移動するノードに移動
            transform.DOMove(nextDestination, 0.4f);

            //待機
            await Task.Delay(TimeSpan.FromSeconds(0.5f));

//            await Task.Run(() => { transform.DOMove(nextDestination, 0.4f); });

            Debug.Log("Before Async.");

//            UnitMoveAnimation(nextDestination);

            path += nextNode.IdX.ToString() + nextNode.IdZ.ToString() + " -> ";

            //次のノードへ
            currentNode = nextNode;
        }

        Debug.Log(path);
        Debug.Log("========================================");

        return true;
    }

//    private async void UnitMoveAnimation(Vector3 nextDestination)
//    {
//        Debug.Log("Inside Async Method.");
//
//        transform.DOMove(nextDestination, 0.4f);
//
//        await Task.Delay(TimeSpan.FromSeconds(0.5f));
//
//        Debug.Log("End Async Method.");
//    }

    private bool ExistUnit(int posX, int posZ)
    {
        Transform player1UnitChildren = GameObject.Find("Player1Units").transform;

        foreach (Transform player1UnitChild in player1UnitChildren)
        {
            if (player1UnitChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                player1UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
            {
                return true;
            }
        }

        Transform player2UnitChildren = GameObject.Find("Player2Units").transform;

        foreach (Transform player2UnitChild in player2UnitChildren)
        {
            if (player2UnitChild.GetComponent<UnitOwnIntPosition>().PosX == posX &&
                player2UnitChild.GetComponent<UnitOwnIntPosition>().PosZ == posZ)
            {
                GameObject surpriseAttacker =
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetActiveUnitInFog(posX, posZ);

                if (surpriseAttacker != null)
                {
                    GameObject.Find("UnitAttackManager")
                        .GetComponent<UnitAttackManager>()
                        .SetSurpriseAttacker(surpriseAttacker);

                    GameObject.Find("UnitAttackManager")
                        .GetComponent<UnitAttackManager>()
                        .SurpriseAttack(gameObject);
                }

                return true;
            }
        }

        return false;
    }
}