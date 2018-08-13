using System.Collections.Generic;
using System;
using DG.Tweening;
using System.Threading.Tasks;
using UnityEngine;

public class UnitMove : MonoBehaviour
{
    public async void MiniMapClickUnitMove(
        int clickMiniMapImageInstancePositionX,
        int clickMiniMapImageInstancePositionZ
    )
    {
        ShortestPath shortestPath = new ShortestPath();

        //ダイクストラ法で最短経路を検索
        Nodes[,] resultNodes =
            shortestPath.DijkstraAlgorithm(
                clickMiniMapImageInstancePositionX,
                clickMiniMapImageInstancePositionZ
                );

        //現在地を目的地として設定
        //ノードが後ろから繋がっているため
        Nodes unitPositionNode =
            resultNodes[
                GetComponent<UnitOwnIntPosition>().PosX,
                GetComponent<UnitOwnIntPosition>().PosZ
            ];

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

            GameObject.Find("FogManager")
                .GetComponent<FogManager>()
                .ClearFog(
                    nextNode.IdX,
                    nextNode.IdZ
                );

            GetComponent<UnitOwnIntPosition>()
                .SetUnitOwnIntPosition(
                    nextNode.IdX,
                    nextNode.IdZ
                );

            //次に移動するノードに移動
            transform.DOMove(nextDestination, 0.4f);

            //待機
            await Task.Delay(TimeSpan.FromSeconds(0.5f));

            path += nextNode.IdX.ToString() + nextNode.IdZ.ToString() + " -> ";

            //次のノードへ
            currentNode = nextNode;
        }

        Debug.Log(path);
        Debug.Log("========================================");
    }

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