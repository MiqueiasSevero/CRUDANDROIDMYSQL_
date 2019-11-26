<?php
include "conexao.php";
$sql="select * from tb_usuario";
$res = $con->query($sql);

while($row = $res->fetch_array(MYSQLI_NUM)){

$dados[] =  array( 'id' => $row[0] ,  'nome' => $row[1] );
		
	
}	

// converte em json

$json = json_encode($dados);
echo ($json);
?>