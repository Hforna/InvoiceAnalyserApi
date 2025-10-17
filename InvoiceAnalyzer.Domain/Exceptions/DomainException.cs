namespace InvoiceAnalyzer.Domain.Exceptions;

public class DomainException : SystemException
{
     private List<string> Errors { get; set; } = [];

     public DomainException(string error) => Errors.Add(error);
     
     public DomainException(List<string> errors) => Errors = errors;
     
     public List<string> GetMessages() => Errors;
}