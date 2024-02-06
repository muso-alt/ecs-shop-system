using System.Collections.Generic;
using System.Text;
using Cysharp.Threading.Tasks;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Newtonsoft.Json;
using ShopComplex.Data;
using ShopComplex.Services;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Networking;

namespace ShopComplex.Systems
{
    public class RequestSystem : IEcsInitSystem
    {
        private EcsCustomInject<ItemsData> _data;
        private EcsCustomInject<SceneService> _sceneService;

        private UnityWebRequest _webRequest;
        private string _jsonText;

        public void Init(IEcsSystems systems)
        {
            StartAsync().Forget();
        }

        private async UniTask StartAsync()
        {
            var json = await LoadRemoteJson();

            _jsonText = json;
            
            /*var yamus = DeserializeYamus(json);
            
            Debug.Log(yamus.id + " | " + yamus.value);
            yamus.value = "Hello World and: ";*/
            
            await PutSomeObjectAsync();
        }

        private async UniTask<string> LoadRemoteJson()
        {
            var webRequest = new UnityWebRequest(_sceneService.Value.URL, "GET");

            webRequest.downloadHandler = new DownloadHandlerBuffer();
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _sceneService.Value.AccessToken);
            
            await webRequest.SendWebRequest();

            if (webRequest.result != UnityWebRequest.Result.Success)
            {
                return string.Empty;
            }
            
            var jsonText = webRequest.downloadHandler.text;
            
            Debug.Log(jsonText);
            
            return jsonText;
        }

        private async UniTask PutSomeObjectAsync()
        {
            //var jsonText = JsonConvert.SerializeObject(yamus);
            var jsonRaw = Encoding.UTF8.GetBytes(_jsonText);
            Debug.Log(_jsonText);

            var webRequest = new UnityWebRequest(_sceneService.Value.URL + "?id=1", "POST");
            
            //webRequest.SetRequestHeader("app-id", "65c25c2cf2fcff03026d959d");
            webRequest.uploadHandler = new UploadHandlerRaw(jsonRaw);
            webRequest.downloadHandler = new DownloadHandlerBuffer();
            
            webRequest.SetRequestHeader("Content-Type", "application/json");
            webRequest.SetRequestHeader("Authorization", "Bearer " + _sceneService.Value.AccessToken);

            await webRequest.SendWebRequest();

            if (webRequest.result == UnityWebRequest.Result.Success)
            {
                Debug.Log("Response: " + webRequest.downloadHandler.text);
            }
            else
            {
                Debug.LogError("Request failed: " + webRequest.error);
            }
        }

        private Yamus DeserializeYamus(string json)
        {
            return JsonConvert.DeserializeObject<Yamus>(json);
        }
    }
    
    public class Yamus
    {
        public Yamus(int id, string value)
        {
            this.id = id;
            this.value = value;
        }
        
        [JsonProperty]
        public int id { get; set; }
        
        [JsonProperty]
        public string value { get; set; }
    }
}