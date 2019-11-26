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
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json; // pacote do json
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "Create")]
    public class Create : Activity
    {
        Button btVoltar, btCad;

        // instancia / objeto / variavel

        CreateInserir inserir = new CreateInserir();
        EditText nome, email, senha;

              



        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.Create);
            // referncia
            btVoltar = FindViewById<Button>(Resource.Id.btVoltar);
            nome = FindViewById<EditText>(Resource.Id.campoNome);
            email = FindViewById<EditText>(Resource.Id.campoEmail);
            senha = FindViewById<EditText>(Resource.Id.campoSenha);
            btCad = FindViewById<Button>(Resource.Id.btCad);

            // evento
            btCad.Click += BtCad_Click;
            btVoltar.Click += BtVoltar_Click;


        }

        private async void BtCad_Click(object sender, EventArgs e)
        {
            // pegar todos os campo e colocar no classe com seus
            //  atributos

            inserir.nome_us  = nome.Text;
            inserir.email_us = email.Text;
            inserir.senha_us = senha.Text;

            // caminho do servido
            string url = "http://10.131.45.51/CRUDANDROID/" +
                "inserir.php";
            HttpClient solicita = new HttpClient();

            // converter em json
            var json = JsonConvert.SerializeObject(inserir);
            Console.WriteLine(" -" + json );

            //prepara para envar e codificar os caracteres
            var conteudoString = new StringContent(json, Encoding.UTF8,
                "application/json");

            //representar o type media
            conteudoString.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            // enviar para o servidor
            HttpResponseMessage res = 
                await solicita.PostAsync(url, conteudoString);

            // pegar a resposta  e poder ler do servidor
             var servResp = await res.Content.ReadAsStringAsync();
            Console.WriteLine(servResp);

            // descompactar o json

            var i = JsonConvert.DeserializeObject<string>(servResp);
            if (i == "certo")
            {
                Toast.MakeText(this, "Sucesso", ToastLength.Short).Show();
            }
            else {
                Toast.MakeText(this, "Erro", ToastLength.Short).Show();

            }
            // limpar campos
            nome.Text = String.Empty;
            email.Text = String.Empty;
            senha.Text = String.Empty;




        }

        private void BtVoltar_Click(object sender, EventArgs e)
        {
            // voltar tela menu
            StartActivity(typeof(MENU));
        }
    }
}