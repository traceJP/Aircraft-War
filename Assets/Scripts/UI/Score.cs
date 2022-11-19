using UnityEngine;
using UnityEngine.UI;
using Utils;

namespace UI
{
    public class Score : Singleton<Score>
    {

        [SerializeField]
        private Text number;

        protected override void Awake()
        {
            base.Awake();
            number.text = "0";
        }

        public void UpdateScore(int score)
        {
            number.text = (int.Parse(number.text) + score).ToString();
        }

    }
}