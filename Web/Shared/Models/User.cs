using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace KratosBlazorApp.Shared.Models
{
    public class LoginRequest
    {
        [JsonPropertyName("identifier")]
        [Required]
        public string UserName { get; set; } = "";

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; } = "";

        public string FlowId { get; set; } = "";

        [JsonPropertyName("csrf_token")]
        public string TokenId { get; set; } = "";

        [JsonPropertyName("method")]
        public string Method { get; set; } = "password";
        public bool RememberMe { get; set; }
    }
    public class RegisterRequest
    {
        public string IdentityId { get; set; } = "";

        [JsonPropertyName("password")]
        [Required]
        public string Password { get; set; } = "";

        [Required]
        public string PasswordConfirm { get; set; } = "";
        [JsonPropertyName("csrf_token")]
        public string TokenId { get; set; } = "";

        [JsonPropertyName("method")]
        public string Method { get; set; } = "password";
        [JsonPropertyName("traits")]
        public RegisterRequestTraits Traits { get; set; } = new();
        
    }


    public class RegisterRequestTraits
    {
        [JsonPropertyName("email")]
        [Required]
        public string Email { get; set; } = "";

        [JsonPropertyName("name")]
        public RegisterRequestTraitsName Name { get; set; } = new();

    }
    public class RegisterRequestTraitsName
    {
   
        [JsonPropertyName("first")]
        public string First { get; set; } = "David";

        [JsonPropertyName("last")]
        public string Last { get; set; } = "House";
    }


    public class CurrentUser
    {
        public bool IsAuthenticated { get; set; }
        public string UserName { get; set; }
        public Dictionary<string, string> Claims { get; set; }
    }
}
