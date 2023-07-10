using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Tilemaps;
using Manager;

namespace UI
{
    public class Pointer : MonoBehaviour
    {
        [SerializeField] private TileBase tile;
        [SerializeField] private TileBase baseTile;
        [SerializeField] private Tilemap tilemap;

        private CoinManager coinManager => CoinManager.Instance;
        private CharaManager charaManager => CharaManager.Instance;
        private ShopButton shopButton;
        //キャラの場合
        private float AdjustX = 0.4f;
        private float AdjustY = 0.5f;
        private GameObject CurrentPutChara;

        private List<Vector3> tileList = new List<Vector3>();
        private Vector3Int pastGrid;

        private void Start()
        {
            shopButton = FindObjectOfType<ShopButton>();
            pastGrid = new Vector3Int(0, 0, 0);
            PointerObservable();
        }

        private void PointerObservable()
        {

            this.UpdateAsObservable()
                .Where(_ => Input.GetMouseButtonDown(0))
                .Where(_ => shopButton.IsOpen == false)
                .Subscribe(_ =>
                {
                    Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                    //タイルマップじゃなくてもキャラでも出来そう。
                    Vector3Int grid = tilemap.WorldToCell(mouse_position);
                /*
                if (tilemap.HasTile(grid)) 
                {
                    
                    //現在選ばれているtileを入れる
                    CurrentPutTile = tile;
                    tilemap.SetTile(grid, CurrentPutTile);

                }*/
                //キャラの場合
                Vector3 gridChara = tilemap.WorldToCell(mouse_position);
                    if (tilemap.HasTile(grid))
                    {
                        CurrentPutChara = charaManager.GetChoiceChara();
                        if (CurrentPutChara == null) return;

                        gridChara = new Vector3(gridChara.x + AdjustX, gridChara.y + AdjustY, gridChara.z);
                        if (checkList(gridChara) == false) return;
                    //値段仮置き
                    if (coinManager.BuyFacility(1) == false) return;

                        Instantiate(CurrentPutChara, gridChara, Quaternion.identity);
                    }
                })
                .AddTo(this);

            //選択タイルを表示  TODO:タイルを戻す
            this.UpdateAsObservable()
               .Where(_ => shopButton.IsOpen == false)
               .Subscribe(_ =>
               {
                   Vector3 mouse_position = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                   Vector3Int grid = tilemap.WorldToCell(mouse_position);
                   if (tilemap.HasTile(pastGrid))
                   {
                       if (grid != pastGrid)
                       {
                           Debug.Log("戻し");
                           tilemap.SetTile(pastGrid, baseTile);
                       }
                   }
                   pastGrid = grid;

                   if (tilemap.HasTile(grid))
                   {
                       if (tilemap.GetTile(grid)!=tile)
                       {
                           Debug.Log("設置");
                           tilemap.SetTile(grid, tile);
                       }
                   }

               })
               .AddTo(this);
        }
        private bool checkList(Vector3 position)
        {
            if (tileList.Contains(position))
            {
                return false;
            }
            else
            {
                tileList.Add(position);
                return true;
            }
        }
    }
}