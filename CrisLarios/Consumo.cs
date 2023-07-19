using System;
using System.Linq;
using System.Threading.Tasks;
using CrisLarios.Helpers;
using Microsoft.Azure.WebJobs;
using Microsoft.Azure.WebJobs.Host;
using Microsoft.Extensions.Logging;

namespace CrisLarios
{
    public static class Consumo
    {
        [FunctionName("Consumo")]
        public static async Task RunAsync([TimerTrigger(" 1 * * * * *")] TimerInfo myTimer, ILogger log)
        {
                
            try
            {
                var date = DateTime.Now;

                string path = @"C:\Users\alexl\OneDrive\Documentos\Area de Trabajo\FrontEndSCA6Plus\data_official.json";

                log.LogInformation("Abriendo Json");
                var response = JsonHelper.desserializar(path);

                if (response.vehicles.Count > 0)
                {
                    var vehiculoList = response.vehicles;
                    var consumoList = response.consumtion_fuel;
                    Random rnd = new Random();

                    int randomIndex = rnd.Next(vehiculoList.Count); //rnd.Next(vehiculoList.Count);

                    var vehiculoRamdon = vehiculoList[randomIndex];

                    var responseCOnsumo = consumoList
                        .Where(x => Convert.ToDateTime(x.Date).Month == date.Month && x.id_vehicle == vehiculoRamdon.id).ToList();
                   
                    var cantGalones = 0;

                    responseCOnsumo.ForEach(x => cantGalones += x.cantidad_gl);

                    if (vehiculoRamdon.assignment > cantGalones)
                    {
                        var result = vehiculoRamdon.assignment - cantGalones;




                        ConsumtionFuel newConsumo = new ConsumtionFuel()
                        {
                            id_consumtio = consumoList.Count + 1,
                            cantidad_gl = new Random().Next(result),
                            Date = date.ToString("dd/MM/yyyy hh:mm:ss"),
                            id_vehicle = vehiculoRamdon.id
                        };

                        consumoList.Add(newConsumo);

                        response.consumtion_fuel = consumoList;

                        log.LogInformation("Guardando Json");

                        JsonHelper.serializar(path, response);

                    }
                    else if (vehiculoRamdon.assignment <= cantGalones)
                    {
                        Console.WriteLine($"Vehiculo: {vehiculoRamdon.id}, No puede consumir mas galones");
                    }


                }
                else
                {
                    Console.Write("No hay vehiculos");
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }


        }
    }
}