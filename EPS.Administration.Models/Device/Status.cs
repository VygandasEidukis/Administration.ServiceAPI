namespace EPS.Administration.Models.Device
{
    public enum Status : int
    {
        ok_service = 0,
        ok_test = 1,
        broken_screen = 2,
        broken_reader = 3,
        broken_keyboard = 4,
        broken_battery = 5,
        broken_other = 6,
        broken_vandal = 7,
        out_of_service = 8,
        out_of_balance = 9,
    }
}