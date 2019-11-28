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
using Newtonsoft.Json;

namespace CRUDANDROIDMYSQL_.Modelo
{
    class CreateInserir
    {




        // cada campo de formulario vai ter um 
        //propriedade/varivel para referenciar o c# via json
        [JsonProperty(PropertyName ="id")]

        public string id_us { get; set; }
        [JsonProperty(PropertyName = "nome")]
        public string nome_us { get; set; }
        [JsonProperty(PropertyName = "email")]
        public string email_us { get; set; }
        [JsonProperty(PropertyName = "senha")]
        public string senha_us { get; set;}
       
        



    }
}