using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Button = UnityEngine.UI.Button;



namespace UI
{
    public class UIManager : MonoBehaviour
    {
        #region -- VARIABLES --
        private Button m_NewGame;
        private Button m_LoadGame;
        private Button m_QuitGame;
        private Button m_Instructions;
        private Button m_Options;
        #endregion

        #region -- VOID FUNCTIONS --
        void OnRallyClick()
        {
            //Function will be use to rally upon click.
            Debug.Log("Begin Rallying ");
        }
        void OnHarvestClick()
        {
            //Function will be use to harvest upon click.
            Debug.Log("Begin Harvesting");
        }
        void OnRecallClick()
        {
            //Function will be use to recall upon click.
            Debug.Log("Recall to barracks.");
        }
        void OnCancelActionClick()
        {
            //Function will cancel previous action upon click.
            Debug.Log("Cancel Previous Action");
        }
        void OnQuitGameClick()
        {
            //Function will quit game upon click.
            Debug.Log("Quit Game");
        }
        #endregion
    }
}
