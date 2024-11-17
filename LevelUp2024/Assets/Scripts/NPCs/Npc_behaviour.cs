using UnityEngine;

public class Npc_behaviour : MonoBehaviour
{
    public string Name;
    public TextAsset InkJSON;
    [SerializeField] private GameObject IndicatorObj;
    public bool Indicator;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        Indicator = false;
    }

    // Update is called once per frame
    void Update()
    {
        if(!Indicator){
            IndicatorObj.SetActive(false);
        }else{
            IndicatorObj.SetActive(true);
        }
    }
}
