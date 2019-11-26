<?php
   include "conexao.php";

//decodificar  em json
$json = file_get_contents('php://input');

// transformar os arquivos array 
$array = json_decode($json,true);
//$array["nome"]= 'dasda'
//$array["email"]= 'sad'

// query
$sql  = "INSERT INTO TB_USUARIO(NOME_US, EMAIL_US, SENHA_US) ";
$sql .= " VALUES('".$array['nome_us']. "' , ";
$sql .= " '".$array['email_us']. "' , ";
$sql .= " '".$array['senha_us']. "') ";
//echo $sql;	
  $resp = array( 0 =>'certo', 1=>'erro');

   if(mysqli_query($con, $sql)){
	    //codificar a respota 
	   echo  json_encode($resp[0],true);//{0:'certo'}
   }
    else{
		echo  json_encode($resp[1],true);//{0:'erro'}
	}

?>