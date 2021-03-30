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
       

        public PublicationConfig(IConfiguration configuration, Database database) : base(configuration)
        {
            _authorIdAndNames = database.Authors.Select(author => KeyValuePair.Create(author.Name,author.Id)).ToList();
            _publisherIdAndNames = database.Publishers.Select(publisher => KeyValuePair.Create(publisher.Name, publisher.Id)).ToList();
        }

        public override Dictionary<string, object> GetConstants()
        {
            Dictionary<string,object> publicationConstants = base.GetConstants();

            publicationConstants.Add("AuthorId", _authorIdAndNames);
            publicationConstants.Add("PublisherId", _publisherIdAndNames);

            return publicationConstants;
        }
    }
}
