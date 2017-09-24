using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.DataAccess
{
    public interface IDishReviewDataAccess
    {
        void SaveDishReview(IDishReview dishReview);
        IDishReview GetDishReview(Guid dishReviewId);
        void DeleteDishReview(Guid dishReviewId);
        IEnumerable<IDishReview> ListDishReviewsByDish(Guid dishId, int maximumResults);
        IEnumerable<IDishReview> ListDishReviewsByUser(Guid userId, int maximumResults);
    }
}