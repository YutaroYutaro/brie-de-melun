using UnityEngine;
using FogDefine;

public class CreateMap : SingletonMonoBehaviour<CreateMap>
{
    //生成するマップオブジェクト
    public GameObject[] MapObjectType;

    public GameObject BlockPrefab;

    public GameObject FogPrefab;

//    public Material FogMaterial;

    //生成するマップの大きさ
    public int MaxPosX = 5;
    public int MaxPosZ = 7;


    //マップ重み表
    private int[,] _mapWeight;
    private int[,] _mapObjectTypeTable;

    // Use this for initialization
    void Start()
    {
        //マップの座標
        _mapWeight = new int[MaxPosX, MaxPosZ];
        _mapObjectTypeTable = new int[MaxPosX, MaxPosZ];

        //左下からマップ生成
        for (int posX = 0; posX < MaxPosX; ++posX)
        {
            for (int posZ = 0; posZ < MaxPosZ; ++posZ)
            {
                //生成するマップオブジェクトを選択
//                int objectNumber = Random.Range(0, MapObjectType.Length);
                int objectNumber = 0;

                if (
                    (posX == 2 && (posZ == 2 || posZ == 4)) ||
                    (posX == 0 && posZ == 6) ||
                    (posX == 4 && posZ == 0)
                )
                {
                    objectNumber = 1;
                }

                if (
                    (posX == 0 && posZ == 0) ||
                    (posX == 2 && posZ == 3) ||
                    (posX == 4 && posZ == 6)
                )
                {
                    objectNumber = 2;
                }

                if (
                    (posX == 0 && posZ == 4) ||
                    (posX == 1 && posZ == 2) ||
                    (posX == 3 && posZ == 4) ||
                    (posX == 4 && posZ == 2)
                )
                {
                    objectNumber = 3;
                }


                //マップオブジェクトごとの重みを保存
                if (MapObjectType[objectNumber].name == "Field")
                {
                    _mapWeight[posX, posZ] = 1;
                    _mapObjectTypeTable[posX, posZ] = 0;
                }
                else if (MapObjectType[objectNumber].name == "Forest")
                {
                    _mapWeight[posX, posZ] = 1;
                    _mapObjectTypeTable[posX, posZ] = 1;
                }
                else if (MapObjectType[objectNumber].name == "GoldMine")
                {
                    _mapWeight[posX, posZ] = 1;
                    _mapObjectTypeTable[posX, posZ] = 2;
                }
                else if (MapObjectType[objectNumber].name == "Mount")
                {
                    _mapWeight[posX, posZ] = 5;
                    _mapObjectTypeTable[posX, posZ] = 3;
                }

                //オブジェクトの生成
                GameObject mapObject = Instantiate(
                    BlockPrefab,
                    new Vector3(posX, 0, posZ),
                    Quaternion.identity
                );

                if (posZ == 0 && (posX == 1 || posX == 2 || posX == 3))
                {
                    mapObject.transform.SetParent(GameObject.Find("ClearMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_NOT_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                    Instantiate(
                        MapObjectType[objectNumber],
                        new Vector3(posX, 0, posZ),
                        Quaternion.identity
                    );
                }
                else if (posZ == 6 && (posX == 1 || posX == 2 || posX == 3))
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_NOT_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );

                    // ToDo: 霧モデルインスタンス
//                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
//                    Instantiate(
//                        FogPrefab,
//                        new Vector3(posX, 1, posZ),
//                        Quaternion.identity
//                    );
                    mapObject.GetComponent<MapObjectController>().InstantiateFog();
                }
                else
                {
                    mapObject.transform.SetParent(GameObject.Find("FoggyMapObjects").transform);
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerOneFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );
                    GameObject.Find("FogManager")
                        .GetComponent<FogManager>()
                        .SetPlayerTwoFogMapState(
                            posX,
                            posZ,
                            Fog.FOG_EXIST
                        );

                    // ToDo: 霧モデルインスタンス
//                    mapObject.GetComponent<Renderer>().material.color = FogMaterial.color;
//                    Instantiate(
//                        FogPrefab,
//                        new Vector3(posX, 1, posZ),
//                        Quaternion.identity
//                    );
                    mapObject.GetComponent<MapObjectController>().InstantiateFog();
                }
            }
        }
    }

    //外部からマップの重み表を取得するメソッド
    public int[,] GetMapWeight()
    {
        return _mapWeight;
    }

    public int GetMapObjectTypeTable(int posX, int posZ)
    {
        return _mapObjectTypeTable[posX, posZ];
    }
}