#nullable disable
using NetTopologySuite.Geometries;

namespace NearMe.Models;

public class StoreLocations
{
    public int Id { get; set; }
    public string LocationName { get; set; }
    public string LocationLatitude { get; set; }
    public string LocationLongitude { get; set; }
    public string LocationAddress { get; set; }
    public Point LocationData { get; set; }
}