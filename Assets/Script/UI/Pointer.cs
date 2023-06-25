using System.Collections.Generic;
using UnityEngine;
using UniRx.Triggers;
using UniRx;
using UnityEngine.Tilemaps;

public class Pointer : MonoBehaviour
{
    [SerializeField] private TileBase tile;
    [SerializeField] private Tilemap tilemap;
    [SerializeField] private GameObject chara;

    //キャラの場合
    private float AdjustX = 0.4f;
    private float AdjustY = 0.5f;
    private TileBase CurrentPutTile;
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

                //タイルマップじゃなくてもキャラでも出来そう。
                Vector3Int grid = tilemap.WorldToCell(mouse_position);

                Debug.Log(grid);
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
                    gridChara = new Vector3(gridChara.x +AdjustX, gridChara.y +AdjustY, gridChara.z);
                    Instantiate(chara, gridChara, Quaternion.identity);
                }
            })
            .AddTo(this);
    }
}
