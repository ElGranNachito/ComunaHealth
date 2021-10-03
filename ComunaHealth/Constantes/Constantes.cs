using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ComunaHealth
{
	/// <summary>
	/// Valores constantes a los que se accede a lo largo de toda la aplicacion
	/// </summary>
	public class Constantes
	{
		public const string NombreRolPaciente = "Paciente";
		public const string NombreRolMedico = "Medico";
		public const string NombreRolAdministrador = "Administrador";
		public const string NombreRolAdministradorjefe = "AdministradorJefe";

		public const string MensajeErrorEsteCampoNoPuedeQuedarVacio = "Este campo no puede quedar vacio";

		public const string NombreBuscadorDefault = "Default";
		public const string NombreBuscadorAdministradores = "Administradores";

		public const string TrueString = "true";
		public const string FalseString = "false";
	}
}
