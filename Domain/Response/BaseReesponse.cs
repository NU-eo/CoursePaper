using OnlineCourses.Domain.Enum;

namespace OnlineCourses.Domain.Response
{
    public class BaseResponse<T> : IBaseResponse<T>
    {
        public string Description { get; set; } = string.Empty;
		public StatusCode StatusCode { get; set; }
		public T Data { get; set; }
    }

    public interface IBaseResponse<T>
    {
        string Description { get; }
        StatusCode StatusCode { get; }
        T Data { get; }
    }
}
