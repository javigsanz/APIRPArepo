using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;

namespace APIRPA.bl
{
    public class Logger
    {
        private static readonly Lazy<Logger> instance = new Lazy<Logger>(() => new Logger());
        private string logDirectoryPath;
        private string logFilePath;
        private Logger()
        {
            // Obtiene la ruta de la carpeta Logs en la raíz del proyecto
            logDirectoryPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Logs");
            // Si la carpeta no existe la crea
            if (!Directory.Exists(logDirectoryPath))
            {
                Directory.CreateDirectory(logDirectoryPath);
            }
            // Define el nombre del archivo de registro
            string fileName = $"log_{DateTime.Now:yyyy-MM-dd_HH-mm-ss}.txt";
            // Combina la ruta de la carpeta con el nombre del archivo
            logFilePath = Path.Combine(logDirectoryPath, fileName);
        }
        public static Logger Instance => instance.Value;
        public void Log (string message)
        {
            try
            {
                using (StreamWriter sw = new StreamWriter(logFilePath, true))
                {
                    sw.WriteLine($"[{DateTime.Now}] - {message}");
                }
            }
            catch (Exception ex) 
            {
                Console.WriteLine($"Error al escribir en el archivo de registro: {ex.Message}");
            }
        }
    }
}