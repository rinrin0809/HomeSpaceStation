using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ShaderManager : MonoBehaviour
{
    public Material defaultMaterial;
    public Material customMaterial;
    public Material renderer;

    [SerializeField]
    private string SkillCheck= "WinterVacation";
    void Start()
    {
        renderer = GetComponent<Material>();

        if (SceneManager.GetActiveScene().name == SkillCheck)
        {
            //renderer.material = customMaterial;
            renderer = customMaterial;
        }
        else
        {
            //renderer.material = defaultMaterial;
            renderer = defaultMaterial;
        }
    }

}
