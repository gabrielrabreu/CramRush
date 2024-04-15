﻿using Cramming.Domain.StaticQuizAggregate;
using Cramming.SharedKernel;

namespace Cramming.UseCases.StaticQuizzes
{
    public interface IStaticQuizRepository : IStaticQuizReadRepository, IRepository<StaticQuiz>
    {
    }
}
