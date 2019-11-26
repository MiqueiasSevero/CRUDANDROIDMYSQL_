using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
// IMPORTAR
using CRUDANDROIDMYSQL_.Modelo;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

namespace CRUDANDROIDMYSQL_
{
    [Activity(Label = "select")]
    public class select : Activity, IEvento
    {
        // CRIA UMA LISTA DE DADOS
        List<SelectJson> dado = new List<SelectJson>();

        // BASEADAPTER PERSONALIZADO
        ListViewBaseAdapter adapter;

        // PARA REFERENCIAR LISTVIEW 
        ListView ltv;

        //COMO ESTAMOS TRABALHANDO COM METODO ASSICRONO QUE ESPECIFICAR async
        protected override async void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //CHAMA A TELA
            SetContentView(Resource.Layout.Select);

            // REFERENCIA O LISTVIEW
            ltv = FindViewById<ListView>(Resource.Id.lsvSelect);

            //ATRIBUI OS DADOS RETORNAN DO AO CLASSE E FORMA DE LIST<>
            dado = await Resgistro();

            //ADAPTAR O DADOS RETORNADO NO LISTVIEW PERSONALIZADO
            adapter = new ListViewBaseAdapter(this, dado);

            // APLICAR INTERFACE COM EVENTO
            adapter.SetEvento(this);

            // FAZ CARREGAR O DADOS DA LIST<> NO LISTVIEW
            ltv.Adapter = adapter;

        }


        //APLICA OS EVENTOS DA INTERFACE

        public void onDelete(string value)
        {
            // VALOR PASSADO PELO IVENTE DELETE QUE ESTA NO LISTVIEWBASEADAPTER
            //Toast.MakeText(this, value, ToastLength.Long).Show();

            // FAZER UM MENSAGEM DE ALERT
            AlertDialog.Builder alert = new AlertDialog.Builder(this);
            // TITULO
            alert.SetTitle("CRUD MYSQL");
            // ICONE
            alert.SetIcon(Android.Resource.Drawable.IcDialogAlert);
            // MENSAGEM 
            alert.SetMessage("DESEJA EXCLUIR");

            //EVENTO
            alert.SetPositiveButton("OK", (senderAlert, args) =>
              {
                  Toast.MakeText(this, value, ToastLength.Long).Show();
                  DeleteRegistro(int.Parse(value));
                  // bool resultado = campo.deleteRegistro(value);
                  //    dado = campo.selecaoPessoa();
                  //    adapter = new ListViewBaseAdapter(this, dado);
                  //    adapter.SetEvento(this);
                  //    ltv.Adapter = adapter;
              });

            alert.SetNegativeButton("NO", (senderAlert, args) =>
            {
                //    adapter = new ListViewBaseAdapter(this, dado);
                //    adapter.SetEvento(this);
                //    ltv.Adapter = adapter;
                //});
                // alert.Show();
                // RECEBER O ID  REGISTRO
                /*
                bool resultado = campo.deleteRegistro(value);
                if (resultado)
                {
                    //mandar carregar de novo os dados
                    dado = campo.selecaoPessoa();
                    adapter = new ListViewBaseAdapter(this, dado);
                    adapter.SetEvento(this);
                    ltv.Adapter = adapter;
                   // ManipulaDados.op.Close();
                }
                else {
                    Toast.MakeText(this, "erro", ToastLength.Long).Show();
                }

            //  //  adapter.NotifyDataSetChanged();
            //    // Modify the data 
            //    //PessoaRepositorio.pes.Remove();

            //    // Notify the ListView about the data change
            //    // adapter.NotifyDataSetChanged();
            //*/
            });

           // alert.Show();

        }

        public void onUpdate(string value)
        {
            //PASSAR PARA A TELA O ID
            /*    Intent pagUpdate = new Intent(this, typeof(UPDATE));
                Bundle parametro = new Bundle();
                parametro.PutString("id", value);
                pagUpdate.PutExtras(parametro);
                StartActivity(pagUpdate);
                */
        }


        // VOLTAR OS REGISTRO DO BANCO
        private async Task<List<SelectJson>> Resgistro()
        {

            string pasta = "http://10.131.45.51/CRUDANDROID/select_.php";

            HttpClient solicita = new HttpClient();

            HttpResponseMessage resultado = await solicita.PostAsync(pasta, null);

            // IsSuccessStatusCode  - Obtém um valor que indica se a resposta HTTP foi bem-sucedida.
            Console.WriteLine(" resp: " + resultado.IsSuccessStatusCode);

            //TRAGA A RESPOSTA EM STRING;
            var content = await resultado.Content.ReadAsStringAsync();
            Console.WriteLine("dados :" + content);

            // DESCOMPACTA O RESPOSTA VENDO SERVIDOR EM FORMA JSON PARA EM FORMA DICTIONARY
            List<SelectJson> i = JsonConvert.DeserializeObject<List<SelectJson>>(content);
            // TESTAR A CONVERSÃO DO JSON DO SERVIDOR
            // TESTAR A CONVERSÃO DO JSON DO SERVIDOR
            Console.WriteLine("respost " + i);
            Console.WriteLine("respost " + i.Count);


            foreach (SelectJson T in i)
            {
                Console.WriteLine("mysql : " + T.ID_US + " - " + T.NOME_US);

            }

            return i;


        }

        // DELETAR REGISTRO DO BANCO
        private async void DeleteRegistro(int id)
        {

            // URIL
            string uri = "http://10.131.45.51/CRUDANDROID/" + "delete.php";

            // CRIA OBJETO DE ENVIO
            HttpClient solicita = new HttpClient();

            // Dicionario ou selecjons
            Dictionary<string, string> i = new Dictionary<string, string>();
            i.Add("id", id.ToString());

            //CONVERTER EM JSON
            var cvJason = JsonConvert.SerializeObject(i);

            // VER COMO FICOU NO JSON
            Console.WriteLine("js " + cvJason);

            //EXISTE 3 CONSTRUTORES
            //O ARQUIVO, TIPO DE COD. CARACATRES, APLICATIVO MIMW
            var contentString = new StringContent(cvJason, Encoding.UTF8, "application/json");

            //Representa um tipo de mídia usado em um cabeçalho Content-Type, conforme definido no RFC
            contentString.Headers.ContentType = new MediaTypeHeaderValue("application/json");

            //ENVIAR O DADOS
            HttpResponseMessage resultado = await solicita.PostAsync(uri, contentString);

            Console.WriteLine(" resp: " + resultado.IsSuccessStatusCode);

            //TRAGA A RESPOSTA EM STRING;
            var content = await resultado.Content.ReadAsStringAsync();


            // saber a volta 
            Console.WriteLine("delete -> " + content);
            // DESCOMPACTA O RESPOSTA VENDO SERVIDOR EM FORMA JSON PARA EM FORMA DICTIONARY
            Dictionary<string, string> servidor = JsonConvert.DeserializeObject<Dictionary<string, string>>(content);

            if (servidor["resp"] == "sucesso")
            {

                Toast.MakeText(this, "DELETADO COM SUCESSO", ToastLength.Long).Show();
                //ATRIBUI OS DADOS RETORNAN DO AO CLASSE E FORMA DE LIST<>
                dado = await Resgistro();

                //ADAPTAR O DADOS RETORNADO NO LISTVIEW PERSONALIZADO
                adapter = new ListViewBaseAdapter(this, dado);

                // APLICAR INTERFACE COM EVENTO
                adapter.SetEvento(this);

            }
            else
            {

                Toast.MakeText(this, "ERROR", ToastLength.Long).Show();
            }



        }
    }
}
