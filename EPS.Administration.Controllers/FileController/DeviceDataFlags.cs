namespace EPS.Administration.Controllers.FileController
{
    /// <summary>
    /// Unique identifiers for file reading
    /// Each flag MUST be unique in ALL of the titles
    /// EOF flag is non unique, that marks end of data set
    /// </summary>
    public enum DeviceDataFlag : int
    {
        EOF = 0,
        Statusai = 1,
        klasifikatorius = 2,
        komplektacijos = 3,
        Vietos = 4,
        Registras = 5,
    }
}
