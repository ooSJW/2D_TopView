using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public partial class UIManager : MonoBehaviour // Data Field
{
    public UIContoller UIContoller { get; private set; }
    private bool damageTextActive = false;
    public bool DamageTextActive
    {
        get => damageTextActive;
        private set
        {
            damageTextActive = value;
        }
    }

}
public partial class UIManager : MonoBehaviour // Initialize
{
    private void Allocate()
    {
        if (!PlayerPrefs.HasKey("DamageTextActive"))
            damageTextActive = true;
        else
            damageTextActive = PlayerPrefs.GetInt("DamageTextActive", 0) == 0 ? false : true;
    }
    public void Initialize()
    {
        Allocate();
        Setup();
    }
    private void Setup()
    {

    }
}

public partial class UIManager : MonoBehaviour // Property
{
    public void SetDamageTextActive(bool value)
    {
        DamageTextActive = value;
        int saveValue = DamageTextActive ? 1 : 0;
        PlayerPrefs.SetInt("DamageTextActive", saveValue);
        PlayerPrefs.Save();
    }
}
public partial class UIManager : MonoBehaviour // Sign
{
    public void SignUpUIController(UIContoller UIContollerValue)
    {
        UIContoller = UIContollerValue;
        UIContoller.Initialize();
    }
    public void SignDownUIController()
    {
        UIContoller = null;
    }
}
