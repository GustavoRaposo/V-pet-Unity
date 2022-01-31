using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace vpet
{
    public class MoveHUD : MonoBehaviour
    {
        public GameObject panelMoveMenu;
        public Button[] buttonMoves;
        private Text[] buttonTexts;

        public event UnityAction<CharMove> OnClickMove;

        void Start()
        {
            panelMoveMenu.SetActive(false);

            buttonTexts = new Text[buttonMoves.Length];
            for (int i = 0; i < buttonMoves.Length; i++) 
            {
                buttonTexts[i] = buttonMoves[i].GetComponentInChildren<Text>();
            }
        }

        public void Show(CharMove[] moves)
        {
            panelMoveMenu.SetActive(true);

            for (int i = 0; i < buttonMoves.Length; i++)
            {
                if (moves.Length > i) 
                {
                    buttonTexts[i].text = moves[i].moveName;
                    CharMove move = moves[i];
                    buttonMoves[i].onClick.AddListener(delegate{OnClickButton(move);});
                }
                else 
                {
                    buttonMoves[i].gameObject.SetActive(false);
                }
            }
        }

        private void OnClickButton(CharMove move)
        {
            OnClickMove?.Invoke(move);
        }

        public void Hide()
        {
            for (int i = 0; i < buttonMoves.Length; i++)
            {
                buttonMoves[i].onClick.RemoveAllListeners();
            }

            panelMoveMenu.SetActive(false);
        }
    }
}