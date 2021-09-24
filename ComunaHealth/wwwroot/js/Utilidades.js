function SeleccionarItemDropdown(indice, idBoton, idInput) {
    switch (indice) {
        case 0:
            CambiarValorSeleccionadoDropdown("Paciente", idBoton, idInput);
            break;
        case 1:
            CambiarValorSeleccionadoDropdown("Doctor", idBoton, idInput);
            break;
        default:
            console.log("Tipo de cuenta no reconocido");
    }
}
function CambiarValorSeleccionadoDropdown(nuevoValor, idBoton, idInput) {
    document.getElementById(idBoton).innerText = nuevoValor;
    document.getElementById(idInput).value = nuevoValor;
}
//# sourceMappingURL=Utilidades.js.map