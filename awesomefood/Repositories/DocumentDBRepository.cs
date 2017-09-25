using System;
using System.Collections.Generic;    
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using Microsoft.Azure.Documents;
using Microsoft.Azure.Documents.Client;
using Microsoft.Azure.Documents.Linq;

namespace AwesomeFood.Repositories
{
    public class DocumentDBRepository<T,T2> : IRepository<T> where T: IEntity where T2: class,T
    {
       
        private readonly string Endpoint;
        private readonly string Key;
        private readonly string DatabaseId;
        private readonly string CollectionId;
        private DocumentClient client;
        public DocumentDBRepository(string endpoint, string key, string databaseId)
        {
            if (string.IsNullOrWhiteSpace(endpoint))
            {
                throw new ArgumentException("DocumentDB Endpoint value is required.", nameof(endpoint));
            }

            if (string.IsNullOrWhiteSpace(key))
            {
                throw new ArgumentException("DocumentDB Key value is required.", nameof(key));
            }

            if (string.IsNullOrWhiteSpace(databaseId))
            {
                throw new ArgumentException("DocumentDB DatabaseId value is required.", nameof(databaseId));
            }

            Endpoint = endpoint;
            Key = key;
            DatabaseId = databaseId;
            CollectionId = typeof(T2).Name;

            client = new DocumentClient(new Uri(Endpoint), Key, new ConnectionPolicy { EnableEndpointDiscovery = false });
            CreateDatabaseIfNotExistsAsync().Wait();
            CreateCollectionIfNotExistsAsync().Wait();
        }
        private async Task<T2> GetItemAsync(string id)
        {
            try
            {
                Document document = await client.ReadDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
                return (T2)(dynamic)document;
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    return default(T2);
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task<IEnumerable<T2>> GetItemsAsync(Expression<Func<T2, bool>> predicate)
        {
            IDocumentQuery<T2> query = client.CreateDocumentQuery<T2>(
                UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId),
                new FeedOptions { MaxItemCount = -1 })
                .Where(predicate)
                .AsDocumentQuery();

            List<T2> results = new List<T2>();
            while (query.HasMoreResults)
            {
                results.AddRange(await query.ExecuteNextAsync<T2>());
            }

            return results;
        }

        private async Task<Document> SaveItemAsync(string id, T item)
        {
            return await client.UpsertDocumentAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId), item, null, true);
        }

        private async Task DeleteItemAsync(string id)
        {
            await client.DeleteDocumentAsync(UriFactory.CreateDocumentUri(DatabaseId, CollectionId, id));
        }

        private async Task CreateDatabaseIfNotExistsAsync()
        {
            try
            {
                await client.ReadDatabaseAsync(UriFactory.CreateDatabaseUri(DatabaseId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDatabaseAsync(new Database { Id = DatabaseId });
                }
                else
                {
                    throw;
                }
            }
        }

        private async Task CreateCollectionIfNotExistsAsync()
        {
            try
            {
                await client.ReadDocumentCollectionAsync(UriFactory.CreateDocumentCollectionUri(DatabaseId, CollectionId));
            }
            catch (DocumentClientException e)
            {
                if (e.StatusCode == System.Net.HttpStatusCode.NotFound)
                {
                    await client.CreateDocumentCollectionAsync(
                        UriFactory.CreateDatabaseUri(DatabaseId),
                        new DocumentCollection { Id = CollectionId },
                        new RequestOptions { OfferThroughput = 1000 });
                }
                else
                {
                    throw;
                }
            }
        }

        public T Get(Guid id)
        {
            return GetItemAsync(id.ToString()).Result;
        }

        public IEnumerable<T> Query(IQueryParameters<T> parameters)
        {
            //TODO: Full blown filtering and sorting
            return GetItemsAsync(p => parameters.Filter(p)).Result;
        }

        public void Save(T entity)
        {
            SaveItemAsync(entity.id.ToString(), entity).Wait();
        }

        public void Delete(Guid id)
        {
            DeleteItemAsync(id.ToString()).Wait();
        }
    }
}