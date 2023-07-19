// Root myDeserializedClass = JsonConvert.DeserializeObject<Root>(myJsonResponse);

using System.Collections.Generic;

public class rootClass
{
    public List<AdministrativeUnit> administrative_units { get; set; }
    public List<Vehicle> vehicles { get; set; }
    public List<Fuel> fuel { get; set; }
    public List<ConsumtionFuel> consumtion_fuel { get; set; }
}

public class AdministrativeUnit
{
    public int id { get; set; }
    public string name { get; set; }
}

public class ConsumtionFuel
{
    public int id_consumtio { get; set; }
    public int cantidad_gl { get; set; }
    public string Date { get; set; }
    public int id_vehicle { get; set; }
}

public class Fuel
{
    public int id { get; set; }
    public string type { get; set; }
    public double quantity { get; set; }
}



public class Vehicle
{
    public int id { get; set; }
    public string plate { get; set; }
    public string assigned_to { get; set; }
    public string brand { get; set; }
    public string gas_type { get; set; }
    public int assignment { get; set; }
    public int id_administrative_units { get; set; }
}