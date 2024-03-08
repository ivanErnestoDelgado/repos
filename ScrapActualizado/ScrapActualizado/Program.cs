using System;
using System.Net;




string direccionUrl = "https://es.wikipedia.org/wiki/Plantilla:Efemérides_-_";

string[] meses = { "_de_enero", "_de_febrero", "_de_marzo", "_de_abril", "_de_mayo", "_de_junio", "_de_julio", "_de_agosto", "_de_septiembre", "_de_octubre", "_de_noviembre", "_de_diciembre" };

string completo = "";
List<DateTime> fechas=new List<DateTime>();
String recopilacion ="";

for (int x = 1; x <= 12; x++)
{
    var dias = System.DateTime.DaysInMonth(2024, x);

    for (int y = 1; y <= dias; y++)
    {
        string urlCambiante = direccionUrl + y + meses[x - 1];
        string elHTML = new WebClient().DownloadString(urlCambiante);
        var parteCierra = 0;
        var parteAbre = 0;
        var wea = "";

        parteAbre = elHTML.IndexOf("<ul>");
        parteCierra = elHTML.IndexOf("</ul>", parteAbre);
        var diferencia = parteCierra - parteAbre;
        var cadena = elHTML.Substring(parteAbre, diferencia);

        string dia = "/" + x + "/" + y;

        parteCierra = 0;
        while (parteCierra != -1)
        {
            parteAbre = cadena.IndexOf(">", parteCierra);
            parteCierra = cadena.IndexOf("<", parteAbre);
            diferencia = parteCierra - parteAbre;
            if (diferencia > 1) wea += cadena.Substring(parteAbre, diferencia);
        }

        wea = wea.Replace(">", String.Empty);
        wea = wea.Replace("&#160;", " ");
        wea = wea.Replace(" (en la imagen)", String.Empty);
        wea = wea.Replace(" ―", "—");
        string[] texto = wea.Split("\n");

        foreach (string prueba in texto)
        {
            string[] aux = prueba.Split("—");
            aux[0] = aux[0].Replace(".", "");
            if (aux[0].Contains(" a C")) aux[0] = "-" + aux[0].Replace(" a C", String.Empty);
            else aux[0] = "+" + aux[0];


            Console.Write(aux[0] + dia + "--" + aux[1] + "\n");
        }
        Thread.Sleep(300);
    }

}
