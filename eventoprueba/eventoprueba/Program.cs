// See https://aka.ms/new-console-template for more information
Console.WriteLine("Hello, World!");
public class Evento
{
    const int minimaLongLinea = 13;
    const int minimaLongDesc = 12;
    public readonly string descripcion;
    public readonly DateTime fecha;
    public readonly bool sucedioDespuesDeCristo;


    public Evento(string linea)
    {

        revisaLongitudLinea(linea);
        sucedioDespuesDeCristo = esEpocaPreCristiana(linea);
        descripcion = encontrarDescripcion(linea);
        fecha = sacarFecha(linea);

    }

    private DateTime sacarFecha(string linea)
    {
        string fechaPropuesta = encotrarFechaPropuesta(linea);
        var fechaDividida = dividirFechaPartes(fechaPropuesta);
        try
        {
            return new DateTime(fechaDividida.anio, fechaDividida.mes, fechaDividida.dia);
        }
        catch (Exception e)
        {
            throw new LineaCorruptaExcepcion();
        }
    }
    private (int anio, int mes, int dia) dividirFechaPartes(string posibleFecha)
    {
        try
        {
            return (Int32.Parse(posibleFecha.Substring(0, 4)), Int32.Parse(posibleFecha.Substring(5, 2)), Int32.Parse(posibleFecha.Substring(8, 2)));

        }
        catch (Exception e)
        {
            throw new LineaCorruptaExcepcion();

        }

    }

    private string encotrarFechaPropuesta(string linea)
    {

        if (esEpocaPreCristiana(linea))
        {
            return linea.Substring(0, 10);
        }

        return linea.Substring(1, 10);

    }


    private string encontrarDescripcion(string linea)
    {
        var indiceDescripcion = linea.IndexOf(" ") + 1;

        return linea.Substring(indiceDescripcion);
    }

    private bool esEpocaPreCristiana(String linea)
    {
        const string indicadorEpocaPreCristiana = "-";
        const int posicionIndicarEpocaPreCristiana = 0;
        var posicion = linea.IndexOf(indicadorEpocaPreCristiana);


        return posicion != posicionIndicarEpocaPreCristiana;
    }

    void revisaLongitudLinea(String linea)
    {
        const int minimaLongLinea = 13;
        if (linea.Length < minimaLongLinea) throw new LineaCorruptaExcepcion();

    }
}



public class MazoEventos
{
    readonly public int cantidadEventosTotal;
    public int cantidadEventosUtilizados;
    public int cantidadEventosRestantes;
    public List<Evento> mazoCartas = new List<Evento>();
    bool estaVacio;

    public MazoEventos(string nombreArchivo)
    {
        string linea;
        try
        {
            using (StreamReader sr = new StreamReader(nombreArchivo))
            {
                
                while ((linea = sr.ReadLine()) != null)
                {
                    if (linea.Trim().EndsWith("."))
                    {
                        Evento evento = new Evento(linea);
                        mazoCartas.Add(evento);
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new ArchivoInvalidoExcepcion();
        }
        cantidadEventosTotal = mazoCartas.Count();
        cantidadEventosRestantes = cantidadEventosTotal;
        estaVacio = ChecarMazo(cantidadEventosUtilizados, cantidadEventosTotal);
        if (estaVacio) throw new ArchivoSinEventosExcepcion();
        mazoCartas = revolverMazo(mazoCartas);
    }

    private List<Evento> revolverMazo(List<Evento> MazoARevolver)
    {
        var rnd = new Random();
        return MazoARevolver.OrderBy(item => rnd.Next()).ToList();
    }

    public Evento repartirCarta()
    {
        estaVacio = ChecarMazo(cantidadEventosUtilizados,cantidadEventosTotal);
        if (!estaVacio)
        {
            cantidadEventosRestantes -=1;
            cantidadEventosUtilizados += 1;
            return agarrarCarta(cantidadEventosRestantes);
        }
        throw new MazoSinEventosExcepcion();
    }

    public Evento agarrarCarta(int posicionEventoAgarrar) => mazoCartas[(posicionEventoAgarrar-1)];

    public bool ChecarMazo(int cantidadEventosUtilizados, int cantidadEventosInicial) => cantidadEventosInicial == cantidadEventosUtilizados;

}



public class LineaCorruptaExcepcion : Exception
{

}
public class ArchivoSinEventosExcepcion : Exception
{

}
public class ArchivoInvalidoExcepcion : Exception
{

}

public class MazoSinEventosExcepcion : Exception
{

}