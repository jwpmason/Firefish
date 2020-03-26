using System.Configuration;

namespace Firefish.Persistance.Config
{
    public class PersistanceConfig : IPersistanceConfig
    {
        public string ClientDbConnectionString { get; set; }

        public PersistanceConfig()
        {
            ClientDbConnectionString = ConfigurationManager.ConnectionStrings["ClaimantDb"].ToString();
        }
    }
}
