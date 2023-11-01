using System.Text;

namespace MicrogrooveChallenge.BLL.Models
{
    public class ModelValidationResult
    {
        public bool IsValid { get; set; }
        public List<string> ErrorMessages { get; set; }

        public ModelValidationResult() { 
            ErrorMessages = new List<string>();
        }

        public string GetFormattedErrorMessage()
        {
            var builder = new StringBuilder();
            builder.AppendLine($"Customer model validation failed:");
            ErrorMessages.ForEach(x => builder.AppendLine(x));
            return builder.ToString();
        }
    }
}
