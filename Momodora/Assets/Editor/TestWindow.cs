using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
#if UNITY_EDITOR
using UnityEditor.UI;
using UnityEditor.Tilemaps;
using UnityEditor.UIElements;
using UnityEditor;
#endif
#if UNITY_EDITOR
//�׽�Ʈ �뵵�� Ȯ�� ������ ������
//�÷��� Ÿ���� �׽�Ʈ�� �ʿ��� �͵��� ��ư�� �����ؼ� ����� ����
public class TestWindow : EditorWindow
{
    /// <summary>
    /// Legacy�ڵ�
    /// </summary>
    //public EnemyCommon_Legacy obj;
    //Queue<GameObject> testList = new Queue<GameObject>();
    //===========================================================================================

    string[] groundTileNames;

    //�޴��� �߰�
    [MenuItem("CustomMenu/BuildTest")]
    static void OpenTestWindow()
    {
        TestWindow window = (TestWindow)EditorWindow.GetWindow(typeof(TestWindow));
        window.Show();
    }

    GameObject basTilePalette;
    TileBase baseTile;
    TileBase escapeTile;

    private void OnEnable()
    {
        string stage01Path = "/Game/TilePalette/Stage01";
        string folderPath = Application.dataPath + stage01Path;
        string[] groundTilePalette = System.IO.Directory.GetFiles(folderPath, "*.prefab");

        groundTileNames = new string[groundTilePalette.Length];

        for (int i = 0; i < groundTilePalette.Length; i++)
        {
            string filename = System.IO.Path.GetFileNameWithoutExtension(groundTilePalette[i]);
            groundTileNames[i] = filename;
            Debug.Log(filename);
            if (filename.Equals("Stage01_BaseTile_RuleTile"))
            {
                basTilePalette = AssetDatabase.LoadAssetAtPath("Assets/Game/TilePalette/Stage01/Stage01_BaseTile_RuleTile.prefab", typeof(GameObject)) as GameObject;
                Tilemap tmp = basTilePalette.GetComponentInChildren<Tilemap>();
                baseTile = tmp.GetTile(new Vector3Int(-6, 0, 0));


                basTilePalette = AssetDatabase.LoadAssetAtPath("Assets/Game/TilePalette/Stage01/Stage01_Others_SpriteTile.prefab", typeof(GameObject)) as GameObject;
                tmp = basTilePalette.GetComponentInChildren<Tilemap>();
                escapeTile = tmp.GetTile(new Vector3Int(-1, 0, 0));

            }
        }

    }

    GameObject mapParent;

    void OnGUI()
    {
        mapParent = EditorGUILayout.ObjectField("�θ� �ʵ�", mapParent, typeof(GameObject), true) as GameObject;

        if (mapParent!=null)
        {

            int currSize = mapParent.transform.childCount;

            Transform before = mapParent.transform;
            if (currSize > 0)
            {
                before = mapParent.transform.GetChild(currSize - 1);
            }
            int type = 1;//horizen

            if (GUILayout.Button("RandomEnemy"))
            {
                Debug.Log(before.name);
                if (currSize > 0)
                {
                    if (type == 1)
                    {
                        Transform a = before.Find("Grid").Find("EscapeRight");
                        a.GetComponent<CompositeCollider2D>().isTrigger = true;
                    }
                    else if (type == 2)
                    {
                        Transform a = before.Find("Grid").Find("EscapeUp");
                        a.GetComponent<CompositeCollider2D>().isTrigger = true;

                    }
                }
                else
                {
                    mapParent.AddComponent<MapData>();
                    type = 0;
                }

                //�⺻���� 1 ,1
                int height = 7;
                int width = 13;

                Vector2Int CenterPos = new Vector2Int(currSize * width * 2, 0);

                //��������, ���� enum���� ����
                int stageIndex = 1;
                int mapIndex = 1;

                mapParent.name = "Stage" + stageIndex + "Map" + mapIndex;

                GameObject map = new GameObject();
                map.transform.parent = mapParent.transform;
                map.name = "Field" + (currSize + 1);
                map.AddComponent<FieldData>().depth = currSize + 1;

                GameObject tileGrid = new GameObject();
                tileGrid.AddComponent<Grid>();
                tileGrid.transform.parent = map.transform;
                tileGrid.name = "Grid";


                GameObject tmpTileMapObject = new GameObject();
                tmpTileMapObject.tag = "Floor";
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                Tilemap tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();
                Rigidbody2D rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
                TilemapCollider2D tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                tilemapCollider.usedByComposite = true;
                CompositeCollider2D compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                compositeCollider.usedByEffector = true;
                PlatformEffector2D platformEffector = tmpTileMapObject.AddComponent<PlatformEffector2D>();
                platformEffector.useOneWay = false;
                compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                compositeCollider.GenerateGeometry();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "BaseTile";


                for (int dy = CenterPos.y - (height); dy < CenterPos.y + height; dy++)
                {
                    for (int dx = CenterPos.x - (width); dx < CenterPos.x + width; dx++)
                    {
                        tmpTilemap.SetTile(new Vector3Int(dx, dy, 0), baseTile);
                    }
                }

                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "ObjectTile";



                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "ScriptTile";


                tmpTileMapObject = new GameObject();
                tmpTileMapObject.tag = "Floor";
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();
                rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
                tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                tilemapCollider.usedByComposite = true;
                compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                compositeCollider.isTrigger = true;
                tmpTileMapObject.AddComponent<SpikeTile>();
                compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                compositeCollider.GenerateGeometry();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "DamagerTile";


                tmpTileMapObject = new GameObject();
                tmpTileMapObject.tag = "Floor";
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();
                rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
                tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                tilemapCollider.usedByComposite = true;
                compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                compositeCollider.usedByEffector = true;
                platformEffector = tmpTileMapObject.AddComponent<PlatformEffector2D>();
                platformEffector.useOneWay = true;
                platformEffector.surfaceArc = 170f;
                compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                compositeCollider.GenerateGeometry();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "ThineTile";


                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "LadderTile";


                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "PaintTile";



                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTileMapObject.AddComponent<TilemapRenderer>();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "WaterArea";

                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTilemap.color = new Color(0f, 0f, 0f, 0f);
                tmpTileMapObject.AddComponent<TilemapRenderer>();
                EscapeTile tmpTile = tmpTileMapObject.AddComponent<EscapeTile>();
                tmpTile.fieldIndex = currSize + 1;
                tmpTile.escapeIndex = 3;
                tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                tilemapCollider.usedByComposite = true;
                rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
                compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                compositeCollider.isTrigger = true;
                compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                compositeCollider.GenerateGeometry();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "EscapeUp";


                TileChangeData tileChangeData = new TileChangeData()
                {
                    tile = escapeTile,
                    transform = Matrix4x4.Translate(new Vector3(.5f, -.5f, 0))
                };
                for (int dx = CenterPos.x - (width); dx < CenterPos.x + width; dx++)
                {
                    Debug.Log("x " + height + "/" + dx);
                    tileChangeData.position = new Vector3Int(dx, height, 0);
                    tmpTilemap.SetTile(tileChangeData, false);
                }


                if (type != 2 || (type == 2 && currSize < 1))
                {
                    tmpTileMapObject = new GameObject();
                    tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                    tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                    tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                    tmpTilemap.color = new Color(0f, 0f, 0f, 0f);
                    tmpTileMapObject.AddComponent<TilemapRenderer>();
                    tmpTile = tmpTileMapObject.AddComponent<EscapeTile>();
                    tmpTile.fieldIndex = currSize + 1;
                    tmpTile.escapeIndex = 2;
                    tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                    tilemapCollider.usedByComposite = true;
                    rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Static;
                    compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                    compositeCollider.isTrigger = true;
                    compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                    compositeCollider.GenerateGeometry();

                    tmpTileMapObject.transform.parent = tileGrid.transform;
                    tmpTileMapObject.name = "EscapeDown";


                    tileChangeData = new TileChangeData()
                    {
                        tile = escapeTile,
                        transform = Matrix4x4.Translate(new Vector3(.5f, -.5f, 0))
                    };
                    for (int dx = CenterPos.x - (width); dx < CenterPos.x + width; dx++)
                    {
                        Debug.Log("x " + height + "/" + dx);
                        tileChangeData.position = new Vector3Int(dx, -(height + 1), 0);
                        tmpTilemap.SetTile(tileChangeData, false);
                    }
                }

                if (type != 1 || (type == 1 && currSize < 1))
                {
                    tmpTileMapObject = new GameObject();
                    tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                    tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                    tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                    tmpTilemap.color = new Color(0f, 0f, 0f, 0f);
                    tmpTileMapObject.AddComponent<TilemapRenderer>();
                    tmpTile = tmpTileMapObject.AddComponent<EscapeTile>();
                    tmpTile.fieldIndex = currSize + 1;
                    tmpTile.escapeIndex = 1;
                    tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                    tilemapCollider.usedByComposite = true;
                    rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                    rb.bodyType = RigidbodyType2D.Static;
                    compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                    compositeCollider.isTrigger = true;
                    compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                    compositeCollider.GenerateGeometry();

                    tmpTileMapObject.transform.parent = tileGrid.transform;
                    tmpTileMapObject.name = "EscapeLeft";


                    tileChangeData = new TileChangeData()
                    {
                        tile = escapeTile,
                        transform = Matrix4x4.Translate(new Vector3(.5f, -.5f, 0))
                    };
                    for (int dy = CenterPos.y - (height); dy < CenterPos.y + height; dy++)
                    {
                        Debug.Log("y " + width + "/" + dy);
                        tileChangeData.position = new Vector3Int(+CenterPos.x - (width + 1), dy, 0);
                        tmpTilemap.SetTile(tileChangeData, false);
                    }
                }

                tmpTileMapObject = new GameObject();
                tmpTileMapObject.transform.position = tmpTileMapObject.transform.position;
                tmpTilemap = tmpTileMapObject.AddComponent<Tilemap>();
                tmpTilemap.tileAnchor = new Vector3(0, 1, 0);
                tmpTilemap.color = new Color(0f, 0f, 0f, 0f);
                tmpTileMapObject.AddComponent<TilemapRenderer>();
                tmpTile = tmpTileMapObject.AddComponent<EscapeTile>();
                tmpTile.fieldIndex = currSize + 1;
                tmpTile.escapeIndex = 0;
                tilemapCollider = tmpTileMapObject.AddComponent<TilemapCollider2D>();
                tilemapCollider.usedByComposite = true;
                rb = tmpTileMapObject.AddComponent<Rigidbody2D>();
                rb.bodyType = RigidbodyType2D.Static;
                compositeCollider = tmpTileMapObject.AddComponent<CompositeCollider2D>();
                compositeCollider.isTrigger = true;
                compositeCollider.generationType = CompositeCollider2D.GenerationType.Manual;
                compositeCollider.GenerateGeometry();

                tmpTileMapObject.transform.parent = tileGrid.transform;
                tmpTileMapObject.name = "EscapeRight";


                tileChangeData = new TileChangeData()
                {
                    tile = escapeTile,
                    transform = Matrix4x4.Translate(new Vector3(.5f, -.5f, 0))
                };
                for (int dy = CenterPos.y - (height); dy < CenterPos.y + height; dy++)
                {
                    Debug.Log("y " + width + "/" + dy);
                    tileChangeData.position = new Vector3Int(+CenterPos.x + width, dy, 0);
                    tmpTilemap.SetTile(tileChangeData, false);
                }



                //MapPalette 

                //TileBase tileBase = 
                //tmpTilemap.SetTile(new Vector3Int(0, 0, 0),   );

            }
        }



        ///<summary>
        /// Legacy�ڵ�
        ///</summary>
            /* if (GUILayout.Button("RandomEnemy"))
             {
                 string[] enemiesName = EnemyPool_Legacy.instance.GetEnemiesName();
                 GameObject tmp = EnemyPool_Legacy.instance.GetEnemy(enemiesName[Random.Range(0, enemiesName.Length)]);
                 tmp.transform.position = Vector3.zero;
                 testList.Enqueue(tmp);
             }

             if (GUILayout.Button("RemoveObject"))
             {
                 GameObject tmp = testList.Dequeue();
                 EnemyPool_Legacy.instance.ReturnEnemy(tmp);
             }


             if (GUILayout.Button("Shoot"))
             {
                 EnemyCommon_Legacy tmp = AssetDatabase.LoadAssetAtPath<EnemyCommon_Legacy>("Assets/Game/Prefabs/Enemies/BigTomata.prefab");

                 Debug.Log(tmp);

                 obj = Instantiate(tmp, new Vector3(0,0,0), Quaternion.identity);
                 obj.Attack();
             }*/
    }
}
#endif