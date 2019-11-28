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

//
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Update")]
    public class Update : Activity
    {
        // campo
        EditText  nome, email, senha;
        TextView id;
        string id_us;
        List<CreateInserir> campo = new List<CreateInserir>();

        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //
            SetContentView(Resource.Layout.Update);

            // referencia
            id = FindViewById<TextView>(Resource.Id.campoId);
            nome = FindViewById<EditText>(Resource.Id.campoNome);
            email = FindViewById<EditText>(Resource.Id.campoEmail);
            senha = FindViewById<EditText>(Resource.Id.campoSenha);

            // pegar o id do usuario que vem da tela listview
            id_us = Intent.GetStringExtra("id");

            // colocar no  edittext
            id.Text = id_us;
            campo  = await  Registro(id_us);

            nome.Text = campo[0].nome_us;
            email.Text = campo[0].email_us;
            senha.Text = campo[0].senha_us;

        }

        // registro
        private async Task<List<CreateInserir>> Registro(string value) {

            // caminho
            string uri =
                "http://10.131.45.51/CRUDANDROID/SelectUpdate.php";

            HttpClient solicita = new HttpClient();
            // array 
            Dictionary<string, string> idDado = 
                new Dictionary<string, string>();
            idDado.Add("id", value);
            // transformar em json
            var json = JsonConvert.SerializeObject(idDado);
            var conteudo = new StringContent(json, Encoding.UTF8,
                "application/json");


            //enviar
            HttpResponseMessage resultado = await solicita.PostAsync(uri, conteudo);
            // saber se esta conectado 
            Console.WriteLine("aqui burro -- " + resultado.IsSuccessStatusCode);

            //ler dados 
            var DadosSevidorJson = await resultado.Content.ReadAsStringAsync();

            // fazer testes da resposta do servidor 
            Console.WriteLine("Dados Json" + DadosSevidorJson);

            //descompactar
            List<CreateInserir> dadosConve = JsonConvert.DeserializeObject<List<CreateInserir>>(DadosSevidorJson);

            foreach (var c in dadosConve)
            {
                Console.WriteLine(c.id_us);
                Console.WriteLine(c.nome_us);
                Console.WriteLine(c.email_us);
                Console.WriteLine(c.senha_us);
            }
            return dadosConve;
        }



    }
}