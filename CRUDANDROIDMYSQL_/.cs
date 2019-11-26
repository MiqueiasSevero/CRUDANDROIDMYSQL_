using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
//importa
using Newtonsoft.Json;


namespace CRUDANDROIDMYSQL_
{
    // classe resposavel de armazenar os valore que vem do servidor em json e converte c#
    class SelectJson
    {
        [JsonProperty(PropertyName = "id")]
        public string ID_US { get; set; }

        [JsonProperty(PropertyName = "nome")]
        public string NOME_US { get; set; }
        
    }
}