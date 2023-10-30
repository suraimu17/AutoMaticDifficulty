using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct SaveData
    {
        /// <summary>
        /// 何番目のプレイか
        /// </summary>
        public int playerNum;

        /// <summary>
        /// 量のスタイルの割合
        /// </summary>
        public float amountStylePer;

        /// <summary>
        /// 要所スタイルの割合
        /// </summary>
        public float strongStylePer;

        /// <summary>
        /// 様子見スタイルの割合
        /// </summary>
        public float saveStylePer;

        /// <summary>
        /// 判断した最も割合があるプレイヤーのスタイル
        /// </summary>
        public string playerStyle;

        /// <summary>
        /// スタイルを持っているか
        /// </summary>
        public bool IsStyle;

    }

    [Serializable]
    public class PlayerDataWrapper
    {
        /// <summary>
        /// プレイヤーのデータを入れるリスト
        /// </summary>
        public List<SaveData> DataList = new List<SaveData>();
    }
}