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

namespace CRUDANDROIDMYSQL_
{

    //// 1º PASSO CRIAR UM INTERFACE
    public interface IEvento
    {
        void onDelete(string value);
        void onUpdate(string value);
    }

    /*
    //     *PARA FAZER UM LISTVIEW CUSTOMIZADA PRECISAMOS 
           DA CLASSE BASEADAPTER
    */
    class ListViewBaseAdapter : BaseAdapter<SelectJson>
    {

        //OBS APARECE ERRO POR MOTIVO QUE TEMOS 
        //DE SOBRECARREGAR OS METODOS DO BASE
        private readonly Activity context;
        private readonly List<SelectJson> usuario;

        //2º CRIAR UM INSTANCIA DO INTERFACE
        IEvento delete;
        IEvento Update;

        //CONSTRUTOR
        public ListViewBaseAdapter(Activity context, 
            List<SelectJson> usuario)
        {
            this.context = context;
            this.usuario = usuario;
        }

        /* 
         * * SOBRECARREGA

             GetItemId – Retorna um identificador 
             de linha (normalmente o número da linha).    
         */
        public override long GetItemId(int position)
        {
            return int.Parse(usuario[position].ID_US);
        }

        


        /*  
         *  sobrecarrega
         *
         *   this[int] indexador – Para retornar os dados 
         *   associados a um  número de linha particular.
         *   
         */
        public override SelectJson this[int position]
        {
            get
            {
                return usuario[position];
            }
        }


        /*
            SOBRECARREGA

              Count – Informa ao controle quantas linhas estão 
              nos dados.


             */

        public override int Count
        {
            get
            {
                return usuario.Count;
            }
        }

        /*
             * SOBRECARREGA

            GetView – Retorna uma View para cada linha,
            preenchida com elemento do xml

            */

        public override View GetView(int position, View convertView,
            ViewGroup parent)
        {
            // layout que tem o modelo do intem com sera mostrado

            var view = convertView ?? context.LayoutInflater.Inflate(
                Resource.Layout.ModeloListView, parent, false);

            var txtid = view.FindViewById<TextView>(Resource.Id.txtid);
            var txtNome = view.FindViewById<TextView>(Resource.Id.txtNome);
            var btEd = view.FindViewById<ImageButton>(Resource.Id.imgEdit);
            var btDel = view.FindViewById<ImageButton>(Resource.Id.imgDel);

            // preencher os campo
            txtNome.Text = usuario[position].NOME_US;
            txtid.Text = usuario[position].ID_US.ToString(); 


            btDel.Click += (sender, arg) =>
            {

                 

                // APLICA INTERFACE
                delete.onDelete(txtid.Text);
            };

            //UPDATE
            btEd.Click += (sender, arg) =>
            {
                //
                Update.onUpdate(txtid.Text);
            };



            return view;
        }

        // 3º passo
        public void SetEvento(IEvento listener)
        {
            this.delete = listener;
            this.Update = listener;

        }

    }
}