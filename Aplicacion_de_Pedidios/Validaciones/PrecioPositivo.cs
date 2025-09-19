namespace Aplicacion_de_Pedidios.Validaciones
{
    public class PrecioPositivo
    {
        public static bool Validar(decimal precio)
        {
            if (precio < 0)
            {
                return false;
            }
            return true;
        }
    }
}
