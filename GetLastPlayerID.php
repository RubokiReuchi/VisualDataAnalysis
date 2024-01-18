<?php
if ($_SERVER["REQUEST_METHOD"] === "POST") {
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
    $sql = "SELECT MAX(`playerID`) AS lastID FROM path";
    $resultado = $conn->query($sql);
    
    if ($resultado->num_rows > 0) {
        $row = $resultado->fetch_assoc();
        $lastID = $row["lastID"];
        if ($lastID > 0) echo $lastID;
        else echo "0";
    } else {
        echo "0";
    }

    // Cerrar la conexión cuando hayas terminado
    $conn->close();

}
else {
    echo "nullData";
}
?>