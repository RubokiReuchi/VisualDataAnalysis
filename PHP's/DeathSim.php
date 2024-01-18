<?php
if ($_SERVER["REQUEST_METHOD"] === "POST") {
    if (isset($_POST["PlayerID"])) {
        $id = $_POST["PlayerID"];
    } else {
        $id = "null";
    }
    
    if (isset($_POST["PositionX"])) {
        $px = $_POST["PositionX"];
    } else {
        $px = "null";
    }
    
    if (isset($_POST["PositionY"])) {
        $py = $_POST["PositionY"];
    } else {
        $py = "null";
    }

    if (isset($_POST["PositionZ"])) {
        $pz = $_POST["PositionZ"];
    } else {
        $pz = "null";
    }

    $servername = "localhost"; // Cambia a la dirección del servidor si es diferente
    $username = "bielrg";
    $password = "JxGaSPddZ78A";
    $database = "bielrg";

    // Crear una conexión
    $conn = new mysqli($servername, $username, $password, $database);

    // Verificar la conexión
    if ($conn->connect_error) {
    die("Error de conexión: " . $conn->connect_error);
    }

    // Consulta a la base de datos
    $sql = "INSERT INTO death(`playerID`, `x`, `y`, `z`) VALUES ('$id', '$px', '$py', '$pz')";
  //  $sql = "INSERT INTO damaged(`x`, `y`, `z`) VALUES (-25,56607, -25,56607, -25,56607)";
    $resultado = $conn->query($sql);

    // Cerrar la conexión cuando hayas terminado
    $conn->close();

}
else {
    echo "nullData";
}
?>