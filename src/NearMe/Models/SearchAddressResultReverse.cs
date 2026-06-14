#nullable disable

using System.Text.Json.Serialization;

namespace NearMe.Models;

public record SearchAddressResultReverse(
    [property: JsonPropertyName("summary")]
    ReverseSummary Summary,

    [property: JsonPropertyName("addresses")]
    IReadOnlyList<AddressResult> Addresses
);

public record ReverseSummary(
    [property: JsonPropertyName("queryTime")]
    int QueryTime,

    [property: JsonPropertyName("numResults")]
    int NumResults
);

public record AddressResult(
    [property: JsonPropertyName("address")]
    AddressDetails Address,
    [property: JsonPropertyName("position")]
    string Position
);

public record AddressDetails(
    [property: JsonPropertyName("streetNumber")]
    string StreetNumber,

    [property: JsonPropertyName("routeNumbers")]
    IReadOnlyList<string> RouteNumbers,

    [property: JsonPropertyName("streetName")]
    string StreetName,

    [property: JsonPropertyName("streetNameAndNumber")]
    string StreetNameAndNumber,

    [property: JsonPropertyName("countryCode")]
    string CountryCode,

    [property: JsonPropertyName("countrySubdivisionCode")]
    string CountrySubdivisionCode,

    [property: JsonPropertyName("countrySubdivisionName")]
    string CountrySubdivisionName,

    [property: JsonPropertyName("municipality")]
    string Municipality,

    [property: JsonPropertyName("postalCode")]
    string PostalCode,

    [property: JsonPropertyName("country")]
    string Country,

    [property: JsonPropertyName("countryCodeISO3")]
    string CountryCodeIso3,

    [property: JsonPropertyName("freeformAddress")]
    string FreeformAddress,

    [property: JsonPropertyName("extendedPostalCode")]
    string ExtendedPostalCode
);
