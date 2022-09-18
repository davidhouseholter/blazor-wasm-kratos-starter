using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace KratosBlazorApp.Shared.Models.Identity
{
    public class OryKratosLoginResponse
    {
        [JsonPropertyName("session")]
        public KratosWhoami Session { get; set; }
    }

    public class OryKratosRegistrationResponse
    {
        [JsonPropertyName("identity")]
        public OryKratosIdentity Identity { get; set; }
        [JsonPropertyName("session")]
        public KratosWhoami Session { get; set; }
    }

    public class OryKratosIdentity
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

    }
    public class KratosWhoami
    {
        [JsonPropertyName("id")]
        public string Id { get; set; }

        [JsonPropertyName("active")]
        public bool Active { get; set; }

        [JsonPropertyName("expires_at")]
        public DateTime ExpiresAt { get; set; }

        [JsonPropertyName("authenticated_at")]
        public DateTime AuthenticatedAt { get; set; }

        [JsonPropertyName("issued_at")]
        public DateTime IssuedAt { get; set; }

        [JsonPropertyName("identity")]
        public OryKratosIdentity Identity { get; set; }
    }

    public class OryKratosRegistrationErrorResponse
    {
        [JsonPropertyName("active")]
        public OryKratosIdentity Active { get; set; }
        [JsonPropertyName("ui")]
        public OryKratosRegistrationErrorUI UI { get; set; }

    }
    public class OryKratosRegistrationErrorUI
    {
        [JsonPropertyName("action")]
        public string Action { get; set; }
        [JsonPropertyName("method")]
        public string Method { get; set; }

        [JsonPropertyName("messages")]
        public List<OryKratosRegistrationErrorUIMessages> Messages { get; set; }
        [JsonPropertyName("nodes")]
        public List<OryKratosRegistrationErrorUINodes> Nodes { get; set; }

    }
    public class OryKratosRegistrationErrorUIMessages
    {
        [JsonPropertyName("context")]
        public object Context { get; set; }
        [JsonPropertyName("id")]
        public long Id { get; set; }
        [JsonPropertyName("text")]
        public string Text { get; set; }
        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

    public class OryKratosRegistrationErrorUINodes
    {
        [JsonPropertyName("attributes")]
        public object Attributes { get; set; }
        [JsonPropertyName("group")]
        public string Group { get; set; }

        [JsonPropertyName("messages")]
        public List<OryKratosRegistrationErrorUIMessages> Messages { get; set; }

        [JsonPropertyName("type")]
        public string Type { get; set; }

    }

    public class OryKratosRegistrationErrorUIMeta
    {

        [JsonPropertyName("label")]
        public OryKratosRegistrationErrorUIMessages Label { get; set; }
    }

    public class IdentityVerificationViewModel
    {
        [JsonPropertyName("message")]
        public string Message { get; set; }

        [JsonPropertyName("event_type")]
        public string EventType { get; set; }
    }
}
