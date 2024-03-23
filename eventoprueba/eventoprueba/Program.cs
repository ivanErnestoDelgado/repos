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



public class MazoEventos : mazoPadre
{
    readonly public int cantidadEventosTotal;
    public int cantidadEventosUtilizados;
    public bool estaVacio;

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
                        mazo.Add(evento);
                    }
                }
            }
        }
        catch (Exception e)
        {
            throw new ArchivoInvalidoExcepcion();
        }
        cantidadEventosTotal = mazo.Count();
        cantidadEventosRestantes = cantidadEventosTotal;
        estaVacio = ChecarMazo(cantidadEventosUtilizados, cantidadEventosTotal);
        if (estaVacio) throw new ArchivoSinEventosExcepcion();
        mazo = revolverMazo(mazo);
    }

    public Evento repartirEvento()
    {
        estaVacio = ChecarMazo(cantidadEventosUtilizados, cantidadEventosTotal);
        if (!estaVacio)
        {
            Evento eventoARepartir = agarrarEvento(cantidadEventosRestantes);
            descartarEvento(cantidadEventosRestantes);
            cantidadEventosRestantes--;
            cantidadEventosUtilizados++;
            return eventoARepartir;
        }
        throw new MazoSinEventosExcepcion();
    }

    //siento que con este metodo va a echar espuma el profe xd
    //esta pensado para recibir el mazo del mazoDescartes y mezclarlo
    public void reiniciarMazo(List<Evento> MazoRecibido)
    {
        mazo = revolverMazo(MazoRecibido);
    }

    public Evento agarrarEvento(int posicionEventoAgarrar) => mazo[(posicionEventoAgarrar - 1)];

    public void descartarEvento(int posicionEventoADescartar)
    {
        mazo.RemoveAt(posicionEventoADescartar - 1);
    }

    public bool ChecarMazo(int cantidadEventosUtilizados, int cantidadEventosInicial) => cantidadEventosInicial == cantidadEventosUtilizados;

}

public class mazoPadre
{
    public int cantidadEventosRestantes;
    public List<Evento> mazo = new List<Evento>();

    public List<Evento> revolverMazo(List<Evento> MazoARevolver)
    {
        var rnd = new Random();
        return MazoARevolver.OrderBy(item => rnd.Next()).ToList();
    }
}

public class mazoDescartes : mazoPadre
{

    //esta pensado para recibir un evento del mazoEventos
    public void recibirEvento(Evento eventoRecibido)
    {
        mazo.Add(eventoRecibido);
    }
    //este metodo sirve para enviar mazo completo y que el mazoEventos los reciba
    public List<Evento> enviarMazo()
    {
        //guardo en otra lista el mazo para poderlo limpiar y devolver el contenido sin problemas
        var mazoAEnviar = agarrarMazo();
        this.mazo.Clear();
        return mazoAEnviar;
    }

    public List<Evento> agarrarMazo()
    {
        return this.mazo;
    }

}

public class Mano
{

    public List<Evento> mano;
    public int tamanio;

    public Mano(List<Evento> eventosRepartidos)
    {
        mano = eventosRepartidos;
        tamanio = mano.Count;
    }

    public List<Evento> mostrarMano() => this.mano;

    public Evento agarrarEvento(int posicionAAgarrar) => mano[(posicionAAgarrar - 1)];


    public bool estaVacio() => (this.mano.Count == 0);

    public void recibirEvento(Evento eventoRecibido)
    {
        mano.Add(eventoRecibido);
    }

    public decision jugarEvento(int posicionEscogida, int indiceEventoEscogido)=> new decision(agarrarEvento(indiceEventoEscogido), posicionEscogida);
    

}
public class decision
{
    public Evento eventoEscogido;
    public int posicionEscogida;
    public decision(Evento EventoEscogido, int posicionEscogida) {
        this.eventoEscogido = EventoEscogido;
        this.posicionEscogida = posicionEscogida;
    }

}

public class linea
{
    public List<Evento> lineaEventos;
    public int posicionesDisponibles;

    public linea(Evento EventoInicial) {
        lineaEventos = new List<Evento>();
        posicionesDisponibles = 2;
        lineaEventos.Add(EventoInicial);

    }

    public void insertarLinea(decision eventoJugado)
    {
        posicionEstaDisponible(eventoJugado.posicionEscogida);
        if (eventoEscogidoEsCorrecto(eventoJugado))
        {
            lineaEventos.Add(eventoJugado.eventoEscogido);
            lineaEventos = lineaEventos.OrderBy(x => x.fecha).ToList<Evento>();
        }
        posicionesDisponibles++;
    }
    public bool eventoEscogidoEsCorrecto(decision eventoJugado)
    {
        
        if (eventoJugado.posicionEscogida > 1)
        {
            if ((eventoJugado.eventoEscogido.fecha > consultarEvento((eventoJugado.posicionEscogida - 1)).fecha)) return true;
        }
        if (eventoJugado.posicionEscogida < posicionesDisponibles)
        {
            if ((eventoJugado.eventoEscogido.fecha < consultarEvento((eventoJugado.posicionEscogida)).fecha)) return true;
        }
        throw new posicionIncorrectaExcepcion();
    }
    

    public Evento consultarEvento(int posicionAConsultar) => lineaEventos[(posicionAConsultar - 1)];

    public void posicionEstaDisponible(int posicionEscogida){
        if ((posicionEscogida > posicionesDisponibles) || (posicionEscogida < 1)) throw new LineaNoTienePosicionExcepcion();
    }
    

}
public class partida
{
    MazoEventos mazoDeEventos;
    mazoDescartes mazoDeDescartes;
    linea lineaDelTiempo;

    partida() { 
    
    
    
    
    }

}

public class EventoCorruptoRecibidoExcepcion : Exception
{

}

public class posicionIncorrectaExcepcion : Exception
{

}

public class LineaNoTienePosicionExcepcion : Exception
{

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