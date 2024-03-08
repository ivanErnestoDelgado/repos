using System;
using System.IO;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;

class efemeride
{
    public int dia, mes, anno;
    public string contenido;
    public efemeride(int anno,int mes, int dia, String texto)
    {
        this.anno = anno;
        this.mes = mes;
        this.dia = dia;
        
        contenido = texto;

        Boolean sucedioDespCristo;
    }

}
class Program
{
    static void Main()
    {

        List<efemeride> listaEfemerides = new List<efemeride>();
        // Ruta del archivo
        string rutaArchivo = "Fechas.txt";

        // Leer todas las líneas del archivo
        string[] lineas = File.ReadAllLines(rutaArchivo);

        // Expresión regular para buscar números en el formato especificado
        string patron = @"(-?\d+)/(\d+)/(\d+)";
        Regex regex = new Regex(patron);

        // Extracción de informacion
        for (int i = 0; i < lineas.Length; i++)
        {   
            //separa el formato de fecha y la efemeride
            string[] separacionefemeride = lineas[i].Split("--");
            //separa cada parte de la fecha usando las diagonales
            string[] separafecha = separacionefemeride[0].Split("/");
            //usando el texto que sacamos lo pasamos a una instancia de la clase efemeride
            efemeride suceso = new efemeride(Int32.Parse(separafecha[0]), Int32.Parse(separafecha[1]), Int32.Parse(separafecha[2]), separacionefemeride[1]);

            listaEfemerides.Add(suceso);
        }

        var listaOrdenada = (from l in listaEfemerides orderby l.anno select l);
        List<string> textofinal = new List<string>();

        foreach (efemeride aux in listaOrdenada) {
            String lineaFinal="";
            
            lineaFinal += aux.anno+"/";
            if (aux.mes.ToString().Length == 1) lineaFinal += "0";
            lineaFinal += aux.mes + "/";
            if (aux.dia.ToString().Length == 1) lineaFinal += "0";
            lineaFinal += aux.dia + aux.contenido;

            // Buscar coincidencias con la expresión regular en la línea actual
            MatchCollection coincidencias = regex.Matches(lineaFinal);

            // Modificar cada coincidencia encontrada
            foreach (Match coincidencia in coincidencias)
            {
                string numero = coincidencia.Groups[1].Value;
                string numeroModificado = PadLeftWithZeroes(numero, 4);
                string nuevaCoincidencia = coincidencia.Value.Replace(numero, numeroModificado);
                lineaFinal = lineaFinal.Replace(coincidencia.Value, nuevaCoincidencia);
            }
            textofinal.Add(lineaFinal);
        }
        // Escribir las líneas modificadas de vuelta en el archivo
        File.WriteAllLines(rutaArchivo, textofinal);

        Console.WriteLine("Números modificados y guardados en el archivo.");
    }

    static string PadLeftWithZeroes(string input, int desiredLength)
    {
        string sign = "";
        if (input.StartsWith("-"))
        {
            sign = "-";
            input = input.Substring(1);
        }
        return sign + input.PadLeft(desiredLength, '0');
    }
}
