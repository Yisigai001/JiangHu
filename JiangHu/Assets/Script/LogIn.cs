using Mono.Cecil.Cil;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using WeChatWASM;

public class LogIn : MonoBehaviour
{
    
    void Start()
    {
        WX.InitSDK((code) =>
            {

            }
        );

        LoginOption login = new LoginOption();
        login.success = (e) =>
        {
            //HttpTool.Instance.Get<LoginData>(Url.login + "?code=" + e.code, data =>
            //{

            //}, false);
        };
        WX.Login(login);
    }

    public void OnStartGame()
    {
        string sceneName = "Home";
        SceneManager.LoadScene(sceneName);
    }
}
