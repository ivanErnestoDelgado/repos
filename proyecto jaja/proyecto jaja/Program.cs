
public class Evento
{
    string descripcion;
    DateTime fecha;
    public bool sucedioDespuesDeCristo;


    public Evento(string linea) {

        descripcion = "";
        fecha = DateTime.now;
        var indice = linea.IndexOf("-");
        sucedioDespuesDeCristo = !(indice==0);
    }

}
