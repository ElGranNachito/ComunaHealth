
function SeleccionarItemDropdown(indice: number, idBoton: string, idInput: string) {

    switch (indice)
    {

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

function CambiarValorSeleccionadoDropdown(nuevoValor: string, idBoton: string, idInput: string)
{
    document.getElementById(idBoton).innerText = nuevoValor;
    (<HTMLInputElement>document.getElementById(idInput)).value = nuevoValor;
}