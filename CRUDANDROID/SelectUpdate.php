<?php

include "conexao.php";

// pega os dado em forma de post
$json = file_get_contents('php://input');

//transforma em array associativo
$array = json_decode($json, true);

$sql="select * from tb_usuario where id_us=".$array['id'];

$res = $con->query($sql);
    
    while($row = $res->fetch_array(MYSQLI_NUM)){
	
	$dados[] =  array( 'id' => $row[0] ,  'nome' => $row[1] , 'email' =>$row[2] , 'senha'=> $row[3]);
    }
        // converte em json

$json = json_encode($dados);
        
echo ($json);
    
$con->close();


?>