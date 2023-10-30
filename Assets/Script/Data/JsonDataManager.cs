using System;
using System.IO;
using UnityEngine;

namespace Data {
    public static class JsonDataManager
    {
        private static string getFilePath() { return Application.dataPath + "/UserData" + ".json"; }

        /// <summary>
        /// �V���A���C�Y����f�[�^
        /// </summary>
        /// <param name="playerDataWrapper"></param>
        public static void Save(PlayerDataWrapper playerDataWrapper) 
        {
            //�V���A���C�Y
            string jsonSerializedData = JsonUtility.ToJson(playerDataWrapper);
            Debug.Log(jsonSerializedData);

           /* //�t�@�C���̏����o��
            StreamWriter writer = new StreamWriter(Application.dataPath + "/savedata.json",false);
            writer.Write(jsonSerializedData);
            writer.Flush();
            writer.Close();*/

            //�t�@�C���̓ǂݍ���
            using (var sw = new StreamWriter(getFilePath(), false)) 
            {
                try
                {
                    sw.Write(jsonSerializedData);
                }
                catch (Exception e) 
                {
                    Debug.Log(e);
                }
            }
        
        }

        /// <summary>
        /// �ǂݍ��݋@�\
        /// </summary>
        /// <returns></returns>
        public static PlayerDataWrapper Load() 
        {
            PlayerDataWrapper jsonDeserializedData = new PlayerDataWrapper();

            try
            {
                //�t�@�C���̓ǂ݂���
                using (FileStream fs = new FileStream(getFilePath(), FileMode.Open))
                using (StreamReader sr = new StreamReader(fs))
                {
                    string result = sr.ReadToEnd();
                    Debug.Log(result);

                    jsonDeserializedData = JsonUtility.FromJson<PlayerDataWrapper>(result);
                }
            }
            catch (Exception e) 
            {
                Debug.Log(e);
            }

            return jsonDeserializedData;
        
        }

    }
}