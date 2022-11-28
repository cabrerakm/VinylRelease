namespace VinylRelease.Models
{
    public class Response<T>
    {
        public int StatusCode { get; set; }
        public string StatusDescription { get; set; }

        public ICollection<T> Data { get; set; }

        public Response(int statusCode_, string statusDescription_, ICollection<T> data_)
        {
            StatusCode = statusCode_;
            StatusDescription = statusDescription_;
            Data = data_;
        }

        public Response() { }
    }
}
