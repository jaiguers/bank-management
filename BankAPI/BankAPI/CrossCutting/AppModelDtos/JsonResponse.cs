namespace BankAPI.CrossCutting.AppModelDtos
{
    public class JsonResponse
    {
        public string Msg { get; set; } = null!;
        public int Status { get; set; }
        public dynamic Errors { get; set; } = null!;
        public dynamic Result { get; set; } = null!;
    }
}
