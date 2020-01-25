namespace Forms.Api
{
    public class RegistrationObject
    {
        public int ErrorLevel { get; set; }
        public string Message { get; set; }
        public DataObject Data { get; set; }

        public override string ToString()
        {
            return $"{Message} | {ErrorLevel} | {Data.Token} | {Data.Login}";
        }
    }
}