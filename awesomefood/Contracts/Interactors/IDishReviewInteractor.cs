using System;
using System.Collections.Generic;
using AwesomeFood.Contracts.Entities;

namespace AwesomeFood.Contracts.Interactors
{
    public interface IDishReviewInteractor
    {
        Guid CreateDishReview(IDishReview dishReview);
        IDishReview GetDishReview(Guid dishReviewId);
        void UpdateDishReview(IDishReview dishReview);
        void DeleteDishReview(Guid dishReviewId);
        IEnumerable<IDishReview> ListDishReviewsByDish(Guid dishId, int maximumResults);
        IEnumerable<IDishReview> ListDishReviewsByUser(Guid userId, int maximumResults);
    }
}