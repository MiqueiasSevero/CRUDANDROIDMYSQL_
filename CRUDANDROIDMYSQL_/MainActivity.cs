using Android.App;
using Android.Widget;
using Android.OS;
// importa 
using System.Net.Http;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "CRUDANDROIDMYSQL_", MainLauncher = false)]
    public class MainActivity : Activity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Set our view from the "main" layout resource
            SetContentView(Resource.Layout.Main);

            Acessa();
        }


        private async void Acessa() {
            //caminho do serividor
            string uri = "http://10.131.46.30:8085/CRUDANDROID/SELECT.php";

            // Objeto instacia que permita fazer a conexao
            HttpClient solicita = new HttpClient();

            // mandor solicitação, servidor devolver a resposta assicrono
            //-> solicita.PostAsync(); INSERIR NOVO REGISTRO (INSERT)
            //-> solicita.PutAsync(); ATUALIZA DADOS NO SERVIDOR(UPDATE)
            //-> solicita.GetAsync(); RECUPERA DADOS NO SERVIÇO WEB(SELECT)
            //-> solicita.DeleteAsync();DELETAR REGISTRO (DELET)
            HttpResponseMessage resultado = await solicita.PostAsync(uri, null);

            //httpResponseMessage - tem um propriedade que indica a reposta do
            // servidor se foi bem sucedida
            Console.WriteLine("RESPOSTA DO XAMMP " + resultado.IsSuccessStatusCode);

            // TRAGA OS DOS COM RESPOSTA DO SERVIDOR

            var conteudo = await resultado.Content.ReadAsStringAsync();

            // mostra os dados vindo do servidor a forma
            Console.WriteLine("DADOS " + conteudo);

            //  FAZER A DESCOMPACTAÇÃO DA RESPOTA VINDA EM JSON
            List<SelectJson> listaRet =
                JsonConvert.DeserializeObject<List<SelectJson>>(conteudo);
            // ler os dados
            foreach (SelectJson dados in listaRet) {

                Console.WriteLine(dados.ID_US + " " + dados.NOME_US);

            }

        }



    }
}

