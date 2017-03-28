using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIActions : MonoBehaviour {
    UI.UIManager uiManager;
    void Awake()
    {
         uiManager= FindObjectOfType<UI.UIManager>();
    }
	// Use this for initialization
	void Start ()
    {
        
        m_ActionsTAB = uiManager.ActionsTab;
        Scalefactor = uiManager.UIScaleFactor;
        revertactionstab = uiManager.RevertActionsTab;

        Assets.Scripts.Managers.EventManager.Subscribe("Actions", OnActions);	
	}

    bool revertactionstab;
    RectTransform m_ActionsTAB;
    float Scalefactor;

    private void OnActions()
    {
        m_ActionsTAB = uiManager.ActionsTab;
        Scalefactor = uiManager.UIScaleFactor;
        revertactionstab = uiManager.RevertActionsTab;

        //If true set values to zero
        if (revertactionstab)
        {
            m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, 0);
            m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, 0);

            revertactionstab = false;
        }
        //If not true set to this position
        else if (!revertactionstab)
        {
            revertactionstab = true;

            m_ActionsTAB.offsetMax = new Vector2(m_ActionsTAB.offsetMax.x, Scalefactor);
            m_ActionsTAB.offsetMin = new Vector2(m_ActionsTAB.offsetMin.x, -115);
        }
        Debug.Log("Move Actions Tab down");
    }

    // Update is called once per frame

}
