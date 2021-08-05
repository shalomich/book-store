using Microsoft.Extensions.Configuration;
using Storage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Storage.Services
{
    public class PublicationConfig : EntityConfig<Publication>
    {
        private readonly IReadOnlyList<KeyValuePair<string, int>> _authorIdAndNames;
        private readonly IReadOnlyList<KeyValuePair<string, int>> _publisherIdAndNames;
       

        public PublicationConfig(IConfiguration configuration, ApplicationContext ApplicationContext) : base(configuration)
        {
            _authorIdAndNames = ApplicationContext.Authors.Select(author => KeyValuePair.Create(author.Name,author.Id)).ToList();
            _publisherIdAndNames = ApplicationContext.Publishers.Select(publisher => KeyValuePair.Create(publisher.Name, publisher.Id)).ToList();
        }

        public override Dictionary<string, object> GetConstants()
        {
            Dictionary<string,object> publicationConstants = base.GetConstants();

            publicationConstants.Add("authorId", _authorIdAndNames);
            publicationConstants.Add("publisherId", _publisherIdAndNames);

            return publicationConstants;
        }
    }
}
