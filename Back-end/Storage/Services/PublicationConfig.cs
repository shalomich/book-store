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
        private readonly Database _database;
        public PublicationConfig(IConfiguration configuration, Database database) : base(configuration)
        {
            _database = database;
        }

        public override Dictionary<string, object> GetConstants()
        {
            var _authorIdAndNames = _database.Authors.Select(author => new { Text = author.Name, Value = author.Id }).ToList();
            var _publisherIdAndNames = _database.Publishers.Select(publisher => new { Text = publisher.Name, Value = publisher.Id }).ToList();

            Dictionary<string,object> publicationConstants = base.GetConstants();

            publicationConstants.Add("authorId", _authorIdAndNames);
            publicationConstants.Add("publisherId", _publisherIdAndNames);

            return publicationConstants;
        }
    }
}
