using System;

public class MazoEventos
{
    string nombreArchivo;
    int cantidadEventosInicial;
    int cantidadEventosRestantes;
    Evento evento;
    bool estaVacio;
    List<Evento> eventos = new List<Evento>();

    MazoEventos(String ruta)
    {
        string[] lineas;
        try
        {
            lineas = File.ReadAllLines(ruta);
        }catch(Exception e)
        {
            throw new ArchivoInvalidoExcepcion();
        }


        foreach (var linea in lineas)
        {
            eventos.add()
        }
    }



    Evento repartirEvento()
    {

        return null;
    }



}

public class ArchivoSinEventoExcepcion : Exception
{

}

public class ArchivoInvalidoExcepcion : Exception
{

}

public class MazoSinEventosExcepcion : Exception
{

}
