using System;
using System.Collections.Generic;
using AwesomeFood.Common;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Repositories;
using AwesomeFood.Repositories;

namespace AwesomeFood.DataAccess
{
    public class DishReviewDataAccess : IDishReviewDataAccess
    {
        private readonly IRepository<IDishReview> _dishReviewRepository;

        public DishReviewDataAccess(IRepository<IDishReview> dishReviewRepository)
        {
            _dishReviewRepository = dishReviewRepository ?? throw new ArgumentNullException(nameof(dishReviewRepository));
        }
        public void DeleteDishReview(Guid dishReviewId)
        {
            _dishReviewRepository.Delete(dishReviewId);
        }

        public IDishReview GetDishReview(Guid dishReviewId)
        {
            return _dishReviewRepository.Get(dishReviewId);
        }

        public IEnumerable<IDishReview> ListDishReviewsByDish(Guid dishId, int maximumResults)
        {
            var queryParams = new QueryParameters<IDishReview>()
            {
                Filter = (dishReview) => dishReview.DishId == dishId,
                OrderByParameters = new OrderByParameters<IDishReview>() { OrderBy = (dishReview) => dishReview.AwesomenessLevel, OrderByDirection = OrderByDirection.Descending },
                Pagination = new PaginationParameters() { MaximumRecords = maximumResults, Offset = 0 }
            };

            return _dishReviewRepository.Query(queryParams);
        }

        public IEnumerable<IDishReview> ListDishReviewsByUser(Guid userId, int maximumResults)
        {
            var queryParams = new QueryParameters<IDishReview>()
            {
                Filter = (dishReview) => dishReview.CreatedByUserId == userId,
                OrderByParameters = new OrderByParameters<IDishReview>() { OrderBy = (dishReview) => dishReview.AwesomenessLevel, OrderByDirection = OrderByDirection.Descending },
                Pagination = new PaginationParameters() { MaximumRecords = maximumResults, Offset = 0 }
            };

            return _dishReviewRepository.Query(queryParams);
        }

        public void SaveDishReview(IDishReview dishReview)
        {
            _dishReviewRepository.Save(dishReview);
        }
    }
}