using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Tilemaps;
using Manager;

public class Pointer : MonoBehaviour
{
   //[SerializeField] private TileBase tile;
    [SerializeField] private Tilemap tilemap;
    //[SerializeField] private GameObject chara;

    private CoinManager coinManager => CoinManager.Instance;
    private CharaManager charaManager => CharaManager.Instance;
    //�L�����̏ꍇ
    private float AdjustX = 0.4f;
    private float AdjustY = 0.5f;
    private GameObject CurrentPutChara;
    private void Start()
    {
        PointerObservable();
    }

    private void PointerObservable() 
    {

        this.UpdateAsObservable()
            .Where(_ => Input.GetMouseButtonDown(0))
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
                    CurrentPutChara= charaManager.GetChoiceChara();
                    if (CurrentPutChara == null) return;
                    //�l�i���u��
                    if (coinManager.BuyFacility(1) == false) return;
                    
                    gridChara = new Vector3(gridChara.x +AdjustX, gridChara.y +AdjustY, gridChara.z);
                    Instantiate(CurrentPutChara, gridChara, Quaternion.identity);
                }
            })
            .AddTo(this);
    }
}
