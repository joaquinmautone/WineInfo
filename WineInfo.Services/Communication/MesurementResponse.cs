using WineInfo.Entities;

namespace WineInfo.Services.Communication
{
    public class MesurementResponse : BaseResponse
    {
        public Mesurement Mesurement { get; private set; }

        public MesurementResponse(bool success, string message, Mesurement mesurement) : base(success, message)
        {
            Mesurement = mesurement;
        }
    }
}
