<?php

include "conexao.php";

// pega os dado em forma de post
$json = file_get_contents('php://input');

//transforma em array associativo
$array = json_decode($json, true);

$sql="delete from tb_usuario where id_us=".$array['id'];
if($con->query($sql)){
    //retorno 
    $re = array('resp'=>'sucesso');
    echo json_encode($re);
}else{
     $re = array('resp'=>'erro');
    echo json_encode($re);
}
 
    
$con->close();


?>