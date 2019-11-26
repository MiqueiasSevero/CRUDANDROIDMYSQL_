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

// pacote do json
using Newtonsoft.Json;

namespace CRUDANDROIDMYSQL_.Modelo
{
    class SelectJson
    {

        // COLOCAR AS PROPRIEDADE QUE O SERVIDOR 
         //IRA RETORNA COM NOME CORRETO PARA ASSIMILAR O JSON

        [JsonProperty(PropertyName = "ID")]
        public string ID_US { get; set; }

        [JsonProperty(PropertyName = "NOME")]
        public string NOME_US { get; set; }



    }
}