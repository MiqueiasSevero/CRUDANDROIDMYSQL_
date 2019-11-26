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
using System.Net.Http;
using System.Net.Http.Headers;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "LOGIN", MainLauncher = true)]
    public class LOGIN : Activity
    {
        EditText campoLogin, campoSenha;
        Button bt;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.LOGIN);
            campoLogin = FindViewById<EditText>(Resource.Id.campoLogin);
            campoSenha = FindViewById<EditText>(Resource.Id.campoSenha);
            bt = FindViewById<Button>(Resource.Id.btlogin);
            // chama o evento 
            bt.Click += Acessa;
        }

        // metodo de conexao;

        private async void Acessa(object sender, EventArgs e) {
            // caminha do servidor ip/dns/
            string url = "http://10.131.46.30:8085/CRUDANDROID/login.php";
            HttpClient solicita = new HttpClient();
            //dicionario ("chave",'value');
            Dictionary<string, string> dados = new Dictionary<string, string>();
            //colocar os campo vindo do Edittext
            dados.Add("login", campoLogin.Text);
            dados.Add("senha", campoSenha.Text);

            // converte para json
           var json = JsonConvert.SerializeObject(dados);
            Console.WriteLine("ver converção" + json);

            // fazer o cabeçalho forçando  json, no aplication

            var contexto = new StringContent(json, Encoding.UTF8,
                "application/json");


            // representa o tipo de arquivo enviado ao servidor
            contexto.Headers.ContentType =
                new MediaTypeHeaderValue("application/json");

            //enviar os dados e espera a resposta do servidor
            HttpResponseMessage resultado =
                await solicita.PostAsync(url, contexto);

            // isSuccessStatusCode - Obem o valor que 
            // indica a resposta do HTTP foi bem sucedida

            Console.WriteLine(" servidor " 
                + resultado.IsSuccessStatusCode);

            // caso o servidor esteja aberto
            if (resultado.IsSuccessStatusCode) {
                // traga a resposta do servidor
                var conteudo =
                    await resultado.Content.ReadAsStringAsync();
                // DESCOMPACTAR O JSON E COLOCAR EM ARRAY ASSOCIATIVO
                Dictionary<string, string> r =
                    JsonConvert.DeserializeObject<
                    Dictionary<string, string>>(conteudo );

                // mostrar 
                Console.WriteLine(r["resp"]);
                if (r["resp"] == "yes")
                {
                    //direcionar para pagina de menu
                    StartActivity(typeof(MENU));
                }
                else {
                    Toast.MakeText(this, "Login/Senha errado",
                        ToastLength.Short).Show();
                }


            }


        }


    }
}