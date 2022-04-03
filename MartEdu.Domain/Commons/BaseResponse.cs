using Newtonsoft.Json;


namespace MartEdu.Domain.Commons
{
    public class BaseResponse<TSource>
    {
        [JsonIgnore]
        public int? Code { get; set; }

        public TSource Data { get; set; }

        public ErrorResponse Error { get; set; }
    }
}
