using System.Collections.Generic;
using System.Net;

//var builder = WebApplication.CreateBuilder(args);

int[] mes = new int[12];
mes[0] = 31;
mes[1] = 29;
mes[2] = 31;
mes[3] = 30;
mes[4] = 31;
mes[5] = 30;
mes[6] = 31;
mes[7] = 31;
mes[8] = 30;
mes[9] = 31;
mes[10] = 30;
mes[11] = 31;

for (int i = 0; i <= 11; i++)
{
    String mesn;
    switch (i)
    {
        case 0:
            mesn = "enero";
            break;
        case 1:
            mesn = "febrero";
            break;
        case 2:
            mesn = "marzo";
            break;
        case 3:
            mesn = "abril";
            break;
        case 4:
            mesn = "mayo";
            break;
        case 5:
            mesn = "junio";
            break;
        case 6:
            mesn = "julio";
            break;
        case 7:
            mesn = "agosto";
            break;
        case 8:
            mesn = "septiembre";
            break;
        case 9:
            mesn = "octubre";
            break;
        case 10:
            mesn = "noviembre";
            break;
        case 11:
            mesn = "diciembre";
            break;
        default:
            mesn = "wtf";
            break;
    }
    for (int j = 1; j <= mes[i]; j++)
    {
        string dia = j.ToString();
        string fecha = dia + " de " + mesn;
        string url = "https://es.wikipedia.org/wiki/Plantilla:Efem%C3%A9rides_-_" + dia + "_de_" + mesn;
        var elHtml = new WebClient().DownloadString(url);
        //Console.WriteLine(elHtml);
        var parteAbre = elHtml.IndexOf("<li>");
        var parteCierra = elHtml.IndexOf("</li>", parteAbre);
        var diferencia = (parteCierra - parteAbre);
        var parteAbre2 = elHtml.LastIndexOf("<li>");

        //Console.WriteLine("{0} {1} {2}", parteAbre, parteCierra, diferencia);
        var cadena = elHtml.Substring(parteAbre, diferencia);
        while (parteAbre != parteAbre2)
        {
            parteAbre = elHtml.IndexOf("<li>", parteCierra);
            parteCierra = elHtml.IndexOf("</li>", parteAbre) + 1;
            diferencia = (parteCierra - parteAbre);
            cadena = cadena + elHtml.Substring(parteAbre, diferencia);
        }
        //Console.WriteLine(cadena);

        var parteEntra = cadena.IndexOf(">") + 1;
        var parteSale = cadena.IndexOf("<", parteEntra);
        diferencia = (parteSale - parteEntra);
        string texto = cadena.Substring(parteEntra, diferencia);
        while ((parteEntra != cadena.LastIndexOf(">")) | (parteSale != cadena.LastIndexOf("<")))
        {

            parteEntra = cadena.IndexOf(">", parteSale);
            parteSale = cadena.IndexOf("<", parteEntra);
            diferencia = (parteSale - (1 + parteEntra));
            texto = texto + cadena.Substring(parteEntra + 1, diferencia);
        }
        //Console.WriteLine(texto);

        var puntoant = 0;
        var last = texto.LastIndexOf(".");
        var parteOpen = texto.IndexOf(".—");
        var Q = texto.IndexOf(".", parteOpen + 1);
        while (texto.IndexOf(".", Q + 1) < texto.IndexOf(".—", Q + 1))
        {
            var T = Q;
            Q = texto.IndexOf(".", T + 1);
        }
        diferencia = Q - puntoant;
        var limpio = texto.Substring(puntoant, diferencia) + "\n";
        while (Q != last)
        {
            puntoant = Q + 1;
            parteOpen = texto.IndexOf(".—", puntoant);
            //Console.WriteLine(parteOpen);
            if (parteOpen < Q)
            {
                Q = last;
                diferencia = Q - puntoant;
                limpio = limpio + texto.Substring(puntoant, diferencia) + "\n";
            }
            else
            {
                Q = texto.IndexOf(".", parteOpen + 1);
                while (texto.IndexOf(".", Q + 1) < texto.IndexOf(".—", Q + 1))
                {
                    var T = Q;
                    Q = texto.IndexOf(".", T + 1);
                }
                if (Q < 0)
                {
                    Q = last;
                    diferencia = (texto.IndexOf("Wikipedia", parteOpen) - puntoant);
                    limpio = limpio + texto.Substring(puntoant, diferencia) + "\n";
                }
                else
                {
                    diferencia = (Q - puntoant);
                    limpio = limpio + texto.Substring(puntoant, diferencia) + "\n";
                }
            }
        }

        Console.WriteLine(fecha + "\n");
        Console.WriteLine(limpio);

    }

}