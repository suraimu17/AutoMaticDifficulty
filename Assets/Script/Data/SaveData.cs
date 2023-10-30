using System;
using System.Collections.Generic;
using UnityEngine;

namespace Data
{
    [Serializable]
    public struct SaveData
    {
        /// <summary>
        /// ���Ԗڂ̃v���C��
        /// </summary>
        public int playerNum;

        /// <summary>
        /// �ʂ̃X�^�C���̊���
        /// </summary>
        public float amountStylePer;

        /// <summary>
        /// �v���X�^�C���̊���
        /// </summary>
        public float strongStylePer;

        /// <summary>
        /// �l�q���X�^�C���̊���
        /// </summary>
        public float saveStylePer;

        /// <summary>
        /// ���f�����ł�����������v���C���[�̃X�^�C��
        /// </summary>
        public string playerStyle;

        /// <summary>
        /// �X�^�C���������Ă��邩
        /// </summary>
        public bool IsStyle;

    }

    [Serializable]
    public class PlayerDataWrapper
    {
        /// <summary>
        /// �v���C���[�̃f�[�^�����郊�X�g
        /// </summary>
        public List<SaveData> DataList = new List<SaveData>();
    }
}