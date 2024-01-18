<?php
/*
    INFO TO SEND HERE:
    -------------------------------------------------
    dataToGet = (damaged/death/hit/path)
    playerInfo = (0 for all, 'n' for specific player)
*/

if ($_SERVER["REQUEST_METHOD"] === "POST") {
    //Get unity info
    if (isset($_POST["dataToGet"])) {
        $dataToGet = $_POST["dataToGet"];
    } else {
        $dataToGet = "null";
    }

    if (isset($_POST["playerInfo"])) {
        $playerInfo = $_POST["playerInfo"];
    } else {
        $playerInfo = "null";
    }

    //Return if 
    if($dataToGet == "null" || $playerInfo == "null"){
        die("Value error!");
    }

    $servername = "localhost"; // Cambia a la dirección del servidor si es diferente
    $username = "bielrg";
    $password = "JxGaSPddZ78A";
    $database = "bielrg";

    // Crear una conexión
    $conn = new mysqli($servername, $username, $password, $database);

    // Verificar la conexión
    if ($conn->connect_error) {
        die("error de conexión: " . $conn->connect_error);
    }
    
    //------------
    //  CREATE QUERY
    //
    
    $getData = "SELECT ";

    if($playerInfo != 0)
        $getData .= "playerID, ";

    $getData .= "x, y, z ";
    
    if($dataToGet == "path" && $playerInfo != 0)
        $getData .= ", rotation ";

    $getData .= "FROM ";

    switch ($dataToGet) {
        case "damaged":
            $getData .= "damaged ";
            break;
        case "death":
            $getData .= "death ";
            break;
        case "hit":
            $getData .= "hit ";
            break;
        case "path":
            $getData .= "path ";
            break;
    }

    if($playerInfo != 0)
        $getData .= "WHERE playerID = $playerInfo";
    
    //------------

    //Get table from DataBase
    $resultado = $conn->query($getData);

    //TODO: Has to be tested
    if($resultado->num_rows > 0){
        if($dataToGet == "path" && $playerInfo != 0){
            while($row = $resultado->fetch_assoc()){
                echo $row["x"] . "," . $row["y"] . "," . $row["z"] . "," . $row["rotation"] . "\n";
            }
        }
        else{
            while($row = $resultado->fetch_assoc()){
                echo $row["x"] . "," . $row["y"] . "," . $row["z"] . "\n";
            }
        }
    }
    else{
        echo "No values (error)";
    }

    // Cerrar la conexión cuando hayas terminado
    $conn->close();

}
else {
    echo "nullData (error)";
}
?>