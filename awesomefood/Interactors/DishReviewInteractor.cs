using System;
using System.Collections.Generic;
using AwesomeFood.Common.Exceptions;
using AwesomeFood.Contracts.DataAccess;
using AwesomeFood.Contracts.Entities;
using AwesomeFood.Contracts.Interactors;

namespace AwesomeFood.Interactors
{
    public class DishReviewInteractor : IDishReviewInteractor
    {
        private readonly IDishReviewDataAccess _dishReviewDataAccess;

        public DishReviewInteractor(IDishReviewDataAccess dishReviewDataAccess)
        {
            _dishReviewDataAccess = dishReviewDataAccess;
        }
        public Guid CreateDishReview(IDishReview dishReview)
        {
            if (dishReview == null)
            {
                throw new ArgumentNullException(nameof(dishReview));
            }

            dishReview.id = Guid.NewGuid();
            dishReview.CreatedOn = DateTime.UtcNow;
            dishReview.ModifiedOn = DateTime.UtcNow;
            
            _dishReviewDataAccess.SaveDishReview(dishReview);

            return dishReview.id;
        }

        public void DeleteDishReview(Guid dishReviewId)
        {
            _dishReviewDataAccess.DeleteDishReview(dishReviewId);
        }

        public IDishReview GetDishReview(Guid dishReviewId)
        {
            var dishReview = _dishReviewDataAccess.GetDishReview(dishReviewId);

            if (dishReview == null)
            {
                throw new EntityNotFoundException(dishReviewId);
            }

            return dishReview;
        }

        public IEnumerable<IDishReview> ListDishReviewsByDish(Guid dishId, int maximumResults)
        {
            return _dishReviewDataAccess.ListDishReviewsByDish(dishId,maximumResults);
        }

        public IEnumerable<IDishReview> ListDishReviewsByUser(Guid userId, int maximumResults)
        {
            return _dishReviewDataAccess.ListDishReviewsByUser(userId,maximumResults);
        }

        public void UpdateDishReview(IDishReview dishReview)
        {
            if (dishReview == null)
            {
                throw new ArgumentNullException(nameof(dishReview));
            }

            var existingDishReview = GetDishReview(dishReview.id);

            UpdateDishReviewFields(existingDishReview, dishReview);

            _dishReviewDataAccess.SaveDishReview(existingDishReview);
        }

        private static void UpdateDishReviewFields(IDishReview existingDishReview, IDishReview dishReview)
        {
           existingDishReview.AwesomenessLevel = dishReview.AwesomenessLevel;
           existingDishReview.Review = dishReview.Review;
           existingDishReview.ModifiedOn = DateTime.UtcNow;
        }
    }
}