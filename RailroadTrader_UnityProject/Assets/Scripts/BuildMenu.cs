using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class BuildMenu : MonoBehaviour
{
    [System.Serializable]
    public struct m_BuildOption 
    {
        public GameObject prefab;
        public Button button;

        public int cost;
    }

    [SerializeField]
    private ObjectPlacement _placement;

    public List<m_BuildOption> m_BuildOptions;


    // Start is called before the first frame update
    void Start()
    {
        foreach (m_BuildOption option in m_BuildOptions)
        {
            option.button.onClick.AddListener(delegate { StartBuildMode(option.prefab); });
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    protected virtual void StartBuildMode(GameObject pPrefab)
    {
        _placement.ActivateBuildmode(pPrefab);
    }
}
