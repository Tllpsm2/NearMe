#nullable disable

using System.Text.Json.Serialization;

namespace NearMe.Models;

public partial class SearchAddressResult
{
    [JsonPropertyName("summary")] public Summary Summary { get; set; }

    [JsonPropertyName("results")] public Result[] Results { get; set; }
}

public partial class Address
{
    [JsonPropertyName("streetNumber")] public string StreetNumber { get; set; }

    [JsonPropertyName("streetName")] public string StreetName { get; set; }

    [JsonPropertyName("municipalitySubdivision")]
    public string MunicipalitySubdivision { get; set; }

    [JsonPropertyName("municipality")] public string Municipality { get; set; }

    [JsonPropertyName("countrySecondarySubdivision")]
    public string CountrySecondarySubdivision { get; set; }

    [JsonPropertyName("countryTertiarySubdivision")]
    public string CountryTertiarySubdivision { get; set; }

    [JsonPropertyName("countrySubdivisionCode")]
    public string CountrySubdivisionCode { get; set; }

    [JsonPropertyName("postalCode")] public string PostalCode { get; set; }

    [JsonPropertyName("extendedPostalCode")] public string ExtendedPostalCode { get; set; }

    [JsonPropertyName("countryCode")] public string CountryCode { get; set; }

    [JsonPropertyName("country")] public string Country { get; set; }

    [JsonPropertyName("countryCodeISO3")] public string CountryCodeIso3 { get; set; }

    [JsonPropertyName("freeformAddress")] public string FreeformAddress { get; set; }

    [JsonPropertyName("countrySubdivisionName")]
    public string CountrySubdivisionName { get; set; }
}

public partial class EntryPoint
{
    [JsonPropertyName("type")] public string Type { get; set; }

    [JsonPropertyName("position")] public Position Position { get; set; }
}

public partial class Position
{
    [JsonPropertyName("lat")] public double Lat { get; set; }

    [JsonPropertyName("lon")] public double Lon { get; set; }
}

public partial class Viewport
{
    [JsonPropertyName("topLeftPoint")] public Position TopLeftPoint { get; set; }

    [JsonPropertyName("btmRightPoint")] public Position BtmRightPoint { get; set; }
}

public partial class Summary
{
    [JsonPropertyName("query")] public string Query { get; set; }

    [JsonPropertyName("queryType")] public string QueryType { get; set; }

    [JsonPropertyName("queryTime")] public long QueryTime { get; set; }

    [JsonPropertyName("numResults")] public long NumResults { get; set; }

    [JsonPropertyName("offset")] public long Offset { get; set; }

    [JsonPropertyName("totalResults")] public long TotalResults { get; set; }

    [JsonPropertyName("fuzzyLevel")] public long FuzzyLevel { get; set; }
}

