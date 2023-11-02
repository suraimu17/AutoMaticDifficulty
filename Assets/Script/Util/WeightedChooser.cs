using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Util
{
    public class WeightedChooser : MonoBehaviour
    {

        private float[] _weight;

        private float _totalWeight;
        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="weights"></param>
        public WeightedChooser(float[] weights)
        {
            _weight = weights;

            for (var i = 0; i < weights.Length; i++)
            {
                _totalWeight += weights[i];
            }
        }

        public int Choose()
        {
            var randomPoint = Random.Range(0, _totalWeight);

            var currentWeight = 0f;
            for (var i = 0; i < _weight.Length; i++)
            {
                currentWeight += _weight[i];

                if (randomPoint < currentWeight)
                {
                    return i;
                }
            }

            return _weight.Length - 1;
        }
    }
}