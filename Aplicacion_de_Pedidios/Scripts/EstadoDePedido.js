//Conseguir estado de pedido sql server
function obtenerEstadoDePedido(pedidoId) {
    pedidoId = pedidoId.trim();
    if (pedidoId === "") {
        alert("Error id del pedido no hallado");
        return;
    }
    else {
        fetch(`/api/pedidos/estado/${pedidoId}`)
            .then(response => {
                if (!response.ok) {
                    throw new Error("Error en la solicitud");
                }
                return response.json();
            })
            .then(data => {
                if (data && data.estado) {
                    document.getElementById("estadoPedido").innerText = `Estado del pedido ${pedidoId}: ${data.estado}`;
                } else {
                    document.getElementById("estadoPedido").innerText = "No se encontró el estado del pedido.";
                }
            })
            .catch(error => {
                console.error("Error al obtener el estado del pedido:", error);
               +99999999066 document.getElementById("estadoPedido").innerText = "Error al obtener el estado del pedido.";
            });
    }
}