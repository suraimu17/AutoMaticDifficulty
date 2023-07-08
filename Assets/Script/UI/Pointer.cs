using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Tilemaps;
using Manager;
using UI;

namespace UI
{
    public class Pointer : MonoBehaviour
    {
        //[SerializeField] private TileBase tile;
        [SerializeField] private Tilemap tilemap;
        //[SerializeField] private GameObject chara;

        private CoinManager coinManager => CoinManager.Instance;
        private CharaManager charaManager => CharaManager.Instance;
        private ShopButton shopButton;
        //�L�����̏ꍇ
        private float AdjustX = 0.4f;
        private float AdjustY = 0.5f;
        private GameObject CurrentPutChara;

        private List<Vector3> tileList = new List<Vector3>();

        private void Start()
        {
            shopButton = FindObjectOfType<ShopButton>();
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

                //�^�C���}�b�v����Ȃ��Ă��L�����ł��o�������B
                Vector3Int grid = tilemap.WorldToCell(mouse_position);

                    Debug.Log(grid);
                /*
                if (tilemap.HasTile(grid)) 
                {
                    
                    //���ݑI�΂�Ă���tile������
                    CurrentPutTile = tile;
                    tilemap.SetTile(grid, CurrentPutTile);

                }*/
                //�L�����̏ꍇ
                Vector3 gridChara = tilemap.WorldToCell(mouse_position);
                    if (tilemap.HasTile(grid))
                    {
                        CurrentPutChara = charaManager.GetChoiceChara();
                        if (CurrentPutChara == null) return;

                        gridChara = new Vector3(gridChara.x + AdjustX, gridChara.y + AdjustY, gridChara.z);
                        if (checkList(gridChara) == false) return;
                    //�l�i���u��
                    if (coinManager.BuyFacility(1) == false) return;

                        Instantiate(CurrentPutChara, gridChara, Quaternion.identity);
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