using Core.Model.Interface.Data;

namespace Core.Model.Interface.Geography
{
    public interface ICountry : IPrimaryKeyInt
    {
        string Name { get; set; }
        string Code { get; set; }
        string Alpha2 { get; set; }
        string Alpha3 { get; set; }
        string Region { get; set; }
        string SubRegion { get; set; }
        string IntermediateRegion { get; set; }
        string RegionCode { get; set; }
        string SubRegionCode { get; set; }
        string IntermediateRegionCode { get; set; }
        bool UsesPostalCode { get; set; }
    }
}