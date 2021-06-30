﻿namespace Pets.Domain.Services.Animals.Cats
{
    using System;
    using System.Threading;
    using System.Threading.Tasks;
    using Commands.Contexts;
    using Entities;
    using Enums;
    using global::Commands.Abstractions;
    using Queries.Abstractions;

    public class CatService : AnimalServiceBase, ICatService
    {
        private readonly IAsyncCommandBuilder _asyncCommandBuilder;



        public CatService(IAsyncQueryBuilder asyncQueryBuilder, IAsyncCommandBuilder asyncCommandBuilder) : base(asyncQueryBuilder)
        {
            _asyncCommandBuilder = asyncCommandBuilder ?? throw new ArgumentNullException(nameof(asyncCommandBuilder));
        }



        public async Task<Cat> CreateCatAsync(
            string name, 
            Breed breed,
            Food favoriteFood,
            decimal weight, 
            CancellationToken cancellationToken = default)
        {
            await CheckIsAnimalWithNameExistAsync(AnimalType.Cat, name, cancellationToken);
            
            var cat = new Cat(name, breed, favoriteFood, weight);

            await _asyncCommandBuilder.CreateAsync(cat, cancellationToken);

            return cat;
        }
    }
}