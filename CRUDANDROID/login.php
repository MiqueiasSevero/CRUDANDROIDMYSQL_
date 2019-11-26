<?php
include("conexao.php");


//decodificar  em json  esta vindo em forma de http
$json = file_get_contents('php://input');

// transforma em array associativo
$array = json_decode($json,true);
// como o php demonstra o json vai ficar
//$array = array('login'=>'adm@admin.com', 'senha'=>'123' );
// sql ()query
//->print_r($array);

$sql  = "select * from tb_usuario where ";
$sql .= "email_us ='".$array['login']."' ";
$sql .= " and senha_us ='".$array['senha']."' ";

//echo $sql;
// executar 
$con->query($sql);
if($con->affected_rows == 1){
	// fazer a resposta para voltar ao c# em forma json
	$rep = array('resp'=>'yes');
	// codificar
	echo json_encode($rep);
}else{
	
	$rep = array('resp'=>'no');
	// codificar
	echo json_encode($rep);
}
$con->close();
?>