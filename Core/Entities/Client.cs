namespace Core.Entities
{
    public class Client : BaseEntity
    {
        public string Name { get; set; }
        public string CpfCnpj { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Address { get; set; }
       
    }
}