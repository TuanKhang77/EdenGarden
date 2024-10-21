namespace EdenGarden_API.Models.Entities
{
    public class Response69<T> : Response
    {
        public T Data { get; set; }
    }   
    public class ResponseVnpay : Response
    {
        public string PaymentMethod { get; set; }
        public string OrderDescription  { get; set; }
        public string OrderId { get; set; }
        public string PaymentId { get; set; }
        public string TransactionId { get; set; }
        public string Token { get; set; }
        public string VnPayResponseCode { get; set; }
    }
    public class Response
    {
        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="Response"/> is success.
        /// </summary>
        /// <value><c>true</c> if success; otherwise, <c>false</c>.</value>
        public bool Success { get; set; }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message { get; set; }
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>The identifier.</value>
        public object Id { get; set; }

        public int RowsAffected { get; set; }
        public int ReturnId { get; set; }
        //public string ReturnStringId { get; set; }
    }
}
