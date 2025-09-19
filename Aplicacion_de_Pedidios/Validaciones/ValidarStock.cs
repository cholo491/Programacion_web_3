namespace Aplicacion_de_Pedidios.Validaciones
{
    public class ValidarStock
    {
       public static bool Validar(int stock)
        {
            if (stock < 0)
            {
                return false;
            }
            return true;
        }
    }
}
